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
        // Handle collision with the player
        if (collision.CompareTag("Player"))
        {
            // Destroy the bullet
            Destroy(gameObject);

            // Optionally, handle player damage here or notify the Player script
            Debug.Log("Player hit by enemy bullet!");
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
