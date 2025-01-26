using UnityEngine;
using UnityEngine.UI;

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

    [Header("Ultimate Ability Settings")]
    [SerializeField] private float ultimateCooldown = 10f; // Cooldown duration for the ultimate ability
    private float ultimateTimer = 0f; // Tracks time since last use
    [SerializeField] private Image ultimateCooldownUI; // Reference to the UI Image for the cooldown indicator

    private Health health; // Reference to the Health component

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void Start()
    {
        // Subscribe to the Health death event
        health.HealthDepleted += OnPlayerDeath;
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        health.HealthDepleted -= OnPlayerDeath;
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        HandleUltimateAbility();
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
        if (Input.GetKey(KeyCode.J)||Input.GetKey(KeyCode.Z) && fireCooldown <= 0f)
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

    void HandleUltimateAbility()
    {
        // Reduce the cooldown timer
        ultimateTimer -= Time.deltaTime;

        // Update the cooldown UI (if assigned)
        if (ultimateCooldownUI != null)
        {
            ultimateCooldownUI.fillAmount = Mathf.Clamp01(ultimateTimer / ultimateCooldown);
        }

        // Check if the ultimate ability key is pressed and the cooldown is finished
        if (Input.GetKeyDown(KeyCode.F) && ultimateTimer <= 0f)
        {
            ActivateUltimateAbility();
            ultimateTimer = ultimateCooldown; // Reset the cooldown timer
        }
    }

    void ActivateUltimateAbility()
    {
        Debug.Log("Ultimate Ability Activated!");

        // Find and destroy all bullets with the "EnemyBullet" tag
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject bullet in enemyBullets)
        {
            Destroy(bullet);
        }

        // Optional: Add visual or audio effects here (e.g., explosion, screen shake)
    }

    private void OnPlayerDeath()
    {
        GameManager.Instance.GameOver("You Died!");
    }
}
