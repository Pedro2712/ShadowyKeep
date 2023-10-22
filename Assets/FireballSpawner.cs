using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float spawnRate = 0.1f;
    private bool shouldSpawnFireballs = true; 

    void Start()
    {
        InvokeRepeating("SpawnFireball", 0.0f, spawnRate);
    }

    void SpawnFireball()
    {
        if (shouldSpawnFireballs)
        {
            Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        }
    }

    public void StartSpawnFireballs()
    {
        shouldSpawnFireballs = true;
    }
    public void StopSpawnFireballs()
    {
        shouldSpawnFireballs = false;
    }
}
