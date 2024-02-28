using UnityEngine;
using UnityEngine.AI;

public class GunSpawner : MonoBehaviour
{
    public GameObject[] gunPrefabs;
    public int spawnCountPerGun = 15;
    public GameObject GunParent;

    private int currentGunIndex = 0;
    private int totalSpawnedCount = 0;

    void Awake()
    {
        SpawnGuns();
    }

    void SpawnGuns()
    {
        

        while (totalSpawnedCount < spawnCountPerGun * gunPrefabs.Length)
        {
            // Get the current gun prefab to spawn
            GameObject currentGunPrefab = gunPrefabs[currentGunIndex];

            // Sample a random position on the NavMesh
            NavMeshHit hit;
            Vector3 centerPosition = transform.position;
            float spawnRadius = 100f;

            // Generate random positions within the specified range
            Vector3 randomOffset = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0f, Random.Range(-spawnRadius, spawnRadius));
            Vector3 randomPosition = centerPosition + randomOffset;

            // Sample a position on the NavMesh from the generated random position
            if (NavMesh.SamplePosition(randomPosition, out hit, 50f, NavMesh.AllAreas))
            {
                randomPosition = hit.position;
            }
            else
            {
                // Failed to find a valid position, skip this iteration
                continue;
            }

            // Instantiate the current gun type at the random position
            GameObject newGun = Instantiate(currentGunPrefab, randomPosition, Quaternion.identity, GunParent.transform);

            // Update counters
            currentGunIndex = (currentGunIndex + 1) % gunPrefabs.Length; // Move to the next gun index in a circular manner
            totalSpawnedCount++;
        }
    }
}
