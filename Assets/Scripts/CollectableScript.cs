using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Access the GameManager and increment the counter
            GameManager.Instance.AddCollectable();

            // Make the collectable disappear
            Destroy(gameObject);
        }
    }
}