using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton pattern so other scripts can easily access GameManager
    public static GameManager Instance;

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

    // Public method to reset the game
    public void ResetGame()
    {
        // Reload the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

