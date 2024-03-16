using UnityEngine;
using UnityEngine.AI;

public class GunSpawner : MonoBehaviour
{
    public GameObject[] gunPrefabs;
    public int spawnCountPerGun = 50;
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
            Vector3 randomOffset = new Vector3(Random.Range(-spawnRadius, spawnRadius), 2f, Random.Range(-spawnRadius, spawnRadius));
            Vector3 randomPosition = centerPosition + randomOffset;

            if (Physics.Raycast(randomPosition + Vector3.up * 1000f, Vector3.down, out RaycastHit raycastHit, Mathf.Infinity, NavMesh.AllAreas))
            {
                // Check if the hit point is on the NavMesh
                if (NavMesh.SamplePosition(raycastHit.point, out hit, 300f, NavMesh.AllAreas))
                {
                    // Instantiate the current gun type at the sampled NavMesh position
                    GameObject newGun = Instantiate(currentGunPrefab, hit.position+ new Vector3(0,0.3f,0), Quaternion.identity, GunParent.transform);

                    // Update counters
                    currentGunIndex = (currentGunIndex + 1) % gunPrefabs.Length; // Move to the next gun index in a circular manner
                    totalSpawnedCount++;
                }
            }
        }
    }
}
