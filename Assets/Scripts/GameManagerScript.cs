using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton pattern so other scripts can easily access GameManager
    public static GameManager Instance;

    public int playerHealth = 100; //Health
    public int playerMaxHealth = 100;

    public string currentLevelName = "TestingCell";

    private bool isPaused = false;

    public int collectableCount = 0;

    private void Awake()
    {
        // Ensure only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
           
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenu")
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (playerHealth <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    // Public method to reset the game
    public void ResetGame()
    {
        // Reload the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddCollectable()
    {
        collectableCount++;
        Debug.Log("Collectables: " + collectableCount);
    }
}

