using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Optional: apply damage here
            Destroy(gameObject);
        }
    }
}