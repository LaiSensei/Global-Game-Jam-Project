using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Editable in Inspector
    private int currentHealth;

    void Start()
    {
        // Initialize current health to max health
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Reduce current health by the damage amount
        currentHealth -= damage;

        // Clamp health to 0 to avoid negative values
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"{gameObject.name} took {damage} damage! Current health: {currentHealth}");

        // Check if the entity has no health left
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle death logic here
        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject); // Destroy the GameObject (can be replaced with death animations, etc.)
    }
}
