using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public Transform player;                     // Assign in Inspector
    public float attackRange = 10f;              // Distance at which turret fires
    public float rotationSpeed = 5f;             // How fast turret rotates to face player

    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireCooldown = 2f;
    private float fireTimer = 0f;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        // Rotate toward player
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // Fire if in range and cooldown elapsed
        fireTimer += Time.deltaTime;
        if (distance <= attackRange && fireTimer >= fireCooldown)
        {
            FireAtPlayer();
            fireTimer = 0f;
        }
    }

    void FireAtPlayer()
    {
        if (fireballPrefab != null && fireballSpawnPoint != null)
        {
            Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
        }
    }
}