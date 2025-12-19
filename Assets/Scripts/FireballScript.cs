using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class FireballScript : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;

    private GameManager gameManager;

    void Start()
    {
        Destroy(gameObject, lifetime);

        // Correct way to access the singleton instance
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Access the instance's playerHealth, not the class directly
            gameManager.playerHealth -= 5;

            if (gameManager.playerHealth <= 0)
            {
                SceneManager.LoadScene(0);
            }

            Destroy(gameObject);
        }
    }
}

