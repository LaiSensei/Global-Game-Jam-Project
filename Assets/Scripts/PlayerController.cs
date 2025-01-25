using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public Vector2 boundaryMin;
    public Vector2 boundaryMax;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab; // Assign your Bullet prefab in the Inspector
    [SerializeField] private Transform bulletSpawnPoint; // Reference to the spawn point
    public float fireRate = 0.2f; // Time between shots (in seconds)
    private float fireCooldown = 0f; // Tracks time until the next shot

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, boundaryMin.x, boundaryMax.x),
            Mathf.Clamp(transform.position.y, boundaryMin.y, boundaryMax.y),
            transform.position.z
        );
    }

    void HandleShooting()
    {
        // Reduce the cooldown timer
        fireCooldown -= Time.deltaTime;

        // Check if the fire button is pressed and cooldown is over
        if (Input.GetKey(KeyCode.Space) && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate; // Reset the cooldown
        }
    }

    void Shoot()
    {
        // Instantiate a bullet at the spawn point's position and rotation
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
