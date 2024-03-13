using UnityEngine;
using UnityEngine.AI;

public class GrenadeSpawner : MonoBehaviour
{
    public GameObject[] bulletPrefabs;
    public int spawnCountPerGun = 30;
    public Transform GrenadeParent;

    private int currentGunIndex = 0;
    private int totalSpawnedCount = 0;

    void Awake()
    {
        SpawnGrenades();
    }

    void SpawnGrenades()
    {

        //Debug.Log("spawining grenade///");
        while (totalSpawnedCount < spawnCountPerGun * bulletPrefabs.Length)
        {
            // Get the current gun prefab to spawn
            GameObject currentGunPrefab = bulletPrefabs[currentGunIndex];
            //currentGunPrefab.transform.localScale *= 2;

            // Sample a random position on the NavMesh
            NavMeshHit hit;
            Vector3 centerPosition = transform.position;
            float spawnRadius = 100f;

            // Generate random positions within the specified range
            Vector3 randomOffset = new Vector3(Random.Range(-spawnRadius, spawnRadius), 2f, Random.Range(-spawnRadius, spawnRadius));
            Vector3 randomPosition = centerPosition + randomOffset;

            // Sample a position on the NavMesh from the generated random position
            if (NavMesh.SamplePosition(randomPosition, out hit, 300f, NavMesh.AllAreas))
            {
                randomPosition = hit.position;
            }
            else
            {
                // Failed to find a valid position, skip this iteration
                continue;
            }

            // Instantiate the current gun type at the random position

            //Debug.Log("instantating...grenade");
            GameObject newGun = Instantiate(currentGunPrefab, randomPosition + new Vector3(0,0.5f,0), Quaternion.identity, GrenadeParent);

            // Update counters
            currentGunIndex = (currentGunIndex + 1) % bulletPrefabs.Length; // Move to the next gun index in a circular manner
            totalSpawnedCount++;
        }
    }
}
