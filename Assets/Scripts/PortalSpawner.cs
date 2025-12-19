using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject portalPrefab; // Assign your portal prefab in Inspector
    private bool portalSpawned = false;

    void Update()
    {
        if (!portalSpawned && GameManager.Instance.collectableCount >= 4)
        {
            SpawnPortal();
        }
    }

    private void SpawnPortal()
    {
        Instantiate(portalPrefab, transform.position, Quaternion.identity);
        portalSpawned = true;
    }
}