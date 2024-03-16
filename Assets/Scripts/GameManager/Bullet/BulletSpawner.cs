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
            Vector3 centerPosition = transform.position;
            float spawnRadius = 100f;

            // Generate random positions within the specified range
            Vector3 randomOffset = new Vector3(Random.Range(-spawnRadius, spawnRadius), 2f, Random.Range(-spawnRadius, spawnRadius));
            Vector3 randomPosition = centerPosition + randomOffset;

            // Sample a position on the NavMesh from the generated random position
            if (Physics.Raycast(randomPosition + Vector3.up * 1000f, Vector3.down, out RaycastHit raycastHit, Mathf.Infinity, NavMesh.AllAreas))
            {
                // Check if the hit point is on the NavMesh
                if (NavMesh.SamplePosition(raycastHit.point, out hit, 300f, NavMesh.AllAreas))
                {
                    // Instantiate the current gun type at the sampled NavMesh position
                    GameObject newGun = Instantiate(currentGunPrefab, hit.position+new Vector3(0,0.5f,0), Quaternion.identity, GunParent.transform);

                    // Update counters
                    currentGunIndex = (currentGunIndex + 1) % bulletPrefabs.Length; // Move to the next gun index in a circular manner
                    totalSpawnedCount++;
                }
            }
        }
    }
}
