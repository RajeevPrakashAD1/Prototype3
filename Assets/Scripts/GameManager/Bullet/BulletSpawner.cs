using UnityEngine;
using UnityEngine.AI;

public class BulletSpawner : MonoBehaviour
{
    public GameObject[] bulletPrefabs;
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

       // Debug.Log("spawining bullet///");
        while (totalSpawnedCount < spawnCountPerGun * bulletPrefabs.Length)
        {
            // Get the current gun prefab to spawn
            GameObject currentGunPrefab = bulletPrefabs[currentGunIndex];
            //currentGunPrefab.transform.localScale *= 2;

            // Sample a random position on the NavMesh
            NavMeshHit hit;
            Vector3 centerPosition = new Vector3(0,0,0);
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
            GameObject newGun = Instantiate(currentGunPrefab, randomPosition + new Vector3(0,0.5f,0), Quaternion.identity, GunParent.transform);

            // Update counters
            currentGunIndex = (currentGunIndex + 1) % bulletPrefabs.Length; // Move to the next gun index in a circular manner
            totalSpawnedCount++;
        }
    }
}
