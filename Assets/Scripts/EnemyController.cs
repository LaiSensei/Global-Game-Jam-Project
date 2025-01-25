using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f; // Speed of movement
    public float boundaryMinY = -4.5f; // Minimum boundary on Y-axis
    public float boundaryMaxY = 4.5f;  // Maximum boundary on Y-axis

    private int direction = 1; // 1 for moving up, -1 for moving down

    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab; // Enemy's bullet prefab
    [SerializeField] private Transform bulletSpawnPoint; // Position to spawn bullets
    public float fireRate = 0.2f; // Time between shots
    private float fireCooldown = 0f; // Tracks time until next shot

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        // Move up or down based on the current direction
        transform.Translate(Vector3.up * moveSpeed * direction * Time.deltaTime);

        // Reverse direction if the enemy reaches the boundaries
        if (transform.position.y >= boundaryMaxY)
        {
            direction = -1; // Move down
        }
        else if (transform.position.y <= boundaryMinY)
        {
            direction = 1; // Move up
        }
    }

    void HandleShooting()
    {
        // Reduce the cooldown timer
        fireCooldown -= Time.deltaTime;

        // Check if it's time to shoot
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate; // Reset cooldown
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Bullet Prefab is missing or null!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Handle collision with player bullets
        /*
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject); 
            Destroy(collision.gameObject); 
        }
        */
    }
}
