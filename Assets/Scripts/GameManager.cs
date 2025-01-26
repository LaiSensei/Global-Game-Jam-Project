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
    [SerializeField] private Button restartButton; // Restart button
    [SerializeField] private Button quitButton; // Quit button

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

    private void Start()
    {
        // Ensure Game Over screen is hidden at the start
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        // Ensure time is running at game start
        Time.timeScale = 1f;

        // Link button functionality
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    public void GameOver(string message)
    {
        if (isGameOver) return; // Prevent multiple GameOver triggers

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

        // Pause the game
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game triggered.");
        Application.Quit(); // Quits the application
    }
}
