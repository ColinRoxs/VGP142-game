using UnityEngine;
using UnityEngine.AI;

public class BooEnemyAI : MonoBehaviour, IDamageable
{
    private NavMeshAgent agent;
    private Transform player;
    private Camera mainCam;
    private Renderer enemyRenderer;

    public GameObject fireballPrefab; 
    public Transform fireballSpawnPoint;
    public float attackRange = 10f;
    public float fireCooldown = 2f;

    private float fireTimer = 0f;

    public int health = 100;
    public GameObject deathEffect;

    public AudioClip shootSound;
    public AudioSource audioSource;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        mainCam = Camera.main;
        enemyRenderer = GetComponent<Renderer>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (player == null || mainCam == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Check if enemy is visible in camera view
        if (IsVisibleToCamera())
        {
            // Freeze movement
            agent.isStopped = true;
        }
        else
        {
            // Chase player
            agent.isStopped = false;
            agent.SetDestination(player.position);

            fireTimer += Time.deltaTime;
            if (distance <= attackRange && fireTimer >= fireCooldown)
            {
                FireAtPlayer();
                fireTimer = 0f;
            }
        }
    }

    bool IsVisibleToCamera()
    {
        if (enemyRenderer == null) return false;

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCam);
        return GeometryUtility.TestPlanesAABB(planes, enemyRenderer.bounds);
    }

    void FireAtPlayer()
    {
        if (fireballPrefab != null && fireballSpawnPoint != null)
        {
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
        }

        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining health: {health}");
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