using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    [Tooltip("Add prefabs here to spawn randomly")]
    public List<GameObject> prefabs = new List<GameObject>();

    void Start()
    {
        if (prefabs == null || prefabs.Count == 0)
        {
            Debug.LogWarning("No prefabs assigned to RandomSpawner!");
            return;
        }

        int index = Random.Range(0, prefabs.Count);

        Instantiate(prefabs[index], transform.position, transform.rotation);
    }
}