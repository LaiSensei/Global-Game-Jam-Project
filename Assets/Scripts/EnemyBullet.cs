using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 5f; // Speed of the bullet
    public float lifetime = 3f; // Time before the bullet is destroyed

    void Start()
    {
        // Destroy the bullet after its lifetime
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"EnemyBullet collided with: {collision.gameObject.name}");
        // Handle collision with the player
        Health targetHealth = collision.GetComponent<Health>();
        if (targetHealth != null && collision.CompareTag("Player"))
        {
            Debug.Log("EnemyBullet hit the Player!");
            targetHealth.TakeDamage(10); // Example damage value
            Destroy(gameObject); // Destroy the bullet
        }

        // Optional: Destroy the bullet if it collides with other objects like walls
        /*
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        */
    }
}
