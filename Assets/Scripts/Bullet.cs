using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Speed of the bullet
    public float lifetime = 2f; // Time before the bullet is destroyed

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the bullet after its lifetime
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime); // Move bullet forward
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Handle collision with enemies
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
