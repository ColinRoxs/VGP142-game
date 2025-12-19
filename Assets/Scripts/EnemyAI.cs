using UnityEngine;
using UnityEngine.AI; 

public class EnemyAI : MonoBehaviour
{
    private Transform player;         // Assign in Inspector
    public float chaseRange = 10f;    // Distance at which enemy starts chasing
    public float stopRange = 12f;     // Distance at which enemy gives up

    private NavMeshAgent agent;
    private bool isChasing = false;

    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireCooldown = 2f;
    private float fireTimer = 0f;

    public int health = 100;
    public GameObject deathEffect;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (!isChasing && distance <= chaseRange)
        {
            // Player entered chase range
            isChasing = true;
        }
        else if (isChasing && distance > stopRange)
        {
            // Player left stop range
            isChasing = false;
            if (agent != null) agent.ResetPath();
        }

        if (isChasing)
        {
            if (agent != null)
            {
                agent.SetDestination(player.position);

                fireTimer += Time.deltaTime;
                if (distance <= chaseRange && fireTimer >= fireCooldown)
                {
                    FireAtPlayer();
                    fireTimer = 0f;
                }
            }
        }
    }

    void FireAtPlayer()
    {
        if (fireballPrefab != null && fireballSpawnPoint != null)
        {
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}