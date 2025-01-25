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
        Debug.Log($"PlayerBullet collided with: {collision.gameObject.name}");
        Health targetHealth = collision.GetComponent<Health>();
        if (targetHealth != null && collision.CompareTag("Enemy"))
        {
            Debug.Log("PlayerBullet hit the Enemy!");
            targetHealth.TakeDamage(20); // Example damage value
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
