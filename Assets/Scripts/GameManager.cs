using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene reloading
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton pattern

    [Header("Game State")]
    public bool isGameOver = false; // Tracks if the game is over

    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverScreen; // Reference to the Game Over UI
    [SerializeField] private Text gameOverText; // Text component for displaying the message
    [SerializeField] private Button restartButton; // Restart button (optional: Quit button could be added here)

    private void Awake()
    {
        // Set up the Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Triggers a game over event with a specific message.
    /// </summary>
    /// <param name="message">Message to display (e.g., Victory or Defeat)</param>
    public void GameOver(string message)
    {
        if (isGameOver) return;

        isGameOver = true;

        // Show Game Over UI
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // Set Game Over Text
        if (gameOverText != null)
        {
            gameOverText.text = message;
        }

        // Pause the game (optional)
        Time.timeScale = 0f; // Stops all time-based actions
    }

    /// <summary>
    /// Restarts the game.
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit Game triggered.");
        Application.Quit();
    }
}
