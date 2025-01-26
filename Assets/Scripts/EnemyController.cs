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
    [SerializeField] private Transform[] bulletSpawnPoints; // Array of spawn points for bullets

    // Individual fire rates for each pattern
    [Header("Fire Rates for Patterns")]
    public float singleShotRate = 1f; // Fire rate for Single Shot
    public float spreadShotRate = 1.5f; // Fire rate for Spread Shot
    public float circularBurstRate = 2f; // Fire rate for Circular Burst

    private float fireCooldown = 0f; // Tracks time until next shot

    private int currentPattern = 0; // Tracks the current bullet pattern
    public float patternSwitchTime = 10f; // Time to stay on each pattern
    private float patternTimer = 0f; // Tracks time for switching patterns

    void Update()
    {
        HandleMovement();
        HandleShooting();
        HandlePatternSwitch();
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
            switch (currentPattern)
            {
                case 0:
                    SingleShot();
                    fireCooldown = singleShotRate; // Reset cooldown for Single Shot
                    break;
                case 1:
                    SpreadShot(5, 45f); // 5 bullets in a 45-degree spread
                    fireCooldown = spreadShotRate; // Reset cooldown for Spread Shot
                    break;
                case 2:
                    CircularBurst(8); // 8 bullets in a circle
                    fireCooldown = circularBurstRate; // Reset cooldown for Circular Burst
                    break;
            }
        }
    }

    void HandlePatternSwitch()
    {
        // Increment the pattern timer
        patternTimer += Time.deltaTime;

        // Switch patterns after the specified time
        if (patternTimer >= patternSwitchTime)
        {
            patternTimer = 0f; // Reset timer
            currentPattern = (currentPattern + 1) % 3; // Cycle through patterns (0 -> 1 -> 2 -> 0)
        }
    }

    void SingleShot()
    {
        // Fire a single bullet from each spawn point
        foreach (Transform spawnPoint in bulletSpawnPoints)
        {
            Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    void SpreadShot(int bulletCount, float angleRange)
    {
        // Fire bullets in a spread pattern
        float angleStep = angleRange / (bulletCount - 1);
        float startAngle = -angleRange / 2;

        foreach (Transform spawnPoint in bulletSpawnPoints)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = startAngle + (i * angleStep);
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                Instantiate(bulletPrefab, spawnPoint.position, rotation);
            }
        }
    }

    void CircularBurst(int bulletCount)
    {
        // Fire bullets in a full circular burst
        float angleStep = 360f / bulletCount;

        foreach (Transform spawnPoint in bulletSpawnPoints)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * angleStep;
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                Instantiate(bulletPrefab, spawnPoint.position, rotation);
            }
        }
    }
}
