using UnityEngine;
using UnityEngine.UI; // Required for UI functionality

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100; // Editable in Inspector
    private int currentHealth;

    [Header("UI Settings")]
    [SerializeField] private Image healthBar; // Optional: Assign this for objects with a health bar

    [Header("Death Settings")]
    [SerializeField] private bool isPlayer = false; // Set true for the player, false for enemies

    // Event for notifying other scripts about health depletion
    public delegate void OnHealthDepleted();
    public event OnHealthDepleted HealthDepleted;

    void Start()
    {
        // Initialize current health to max health
        currentHealth = maxHealth;

        // Update the health bar if one is assigned
        if (healthBar != null)
        {
            UpdateHealthBar();
        }
    }

    public void TakeDamage(int damage)
    {
        // Reduce current health by the damage amount
        currentHealth -= damage;

        // Clamp health to 0 to avoid negative values
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"{gameObject.name} took {damage} damage! Current health: {currentHealth}");

        // Update the health bar if one is assigned
        if (healthBar != null)
        {
            UpdateHealthBar();
        }

        // Check if the entity has no health left
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        // Update the fill amount of the health bar based on the current health
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    private void Die()
    {
        // Trigger the HealthDepleted event (if subscribed)
        HealthDepleted?.Invoke();

        // Notify the GameManager directly (if applicable)
        if (isPlayer)
        {
            GameManager.Instance.GameOver("You Died!");
        }
        else
        {
            GameManager.Instance.GameOver("You Won!");
        }

        Debug.Log($"{gameObject.name} has died!");

        // Add additional death logic here (e.g., animations, effects)

        // Destroy the GameObject if it's an enemy
        if (!isPlayer)
        {
            Destroy(gameObject);
        }
    }

    // Optional: Add a method to heal
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"{gameObject.name} healed for {healAmount}! Current health: {currentHealth}");

        // Update the health bar if one is assigned
        if (healthBar != null)
        {
            UpdateHealthBar();
        }
    }
}
