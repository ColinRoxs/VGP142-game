using UnityEngine;

public class Collectable : MonoBehaviour
{
    public AudioClip shootSound;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Access the GameManager and increment the counter
            GameManager.Instance.AddCollectable();

            if (shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }

            // Make the collectable disappear
            Destroy(gameObject);
        }
    }
}