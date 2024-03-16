using UnityEngine;
using UnityEngine.AI;

public class GrenadeSpawner : MonoBehaviour
{
    public GameObject[] grenadePrefabs;
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
        while (totalSpawnedCount < spawnCountPerGun * grenadePrefabs.Length)
        {
            // Get the current gun prefab to spawn
            GameObject currentGunPrefab = grenadePrefabs[currentGunIndex];
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
                    GameObject newGun = Instantiate(currentGunPrefab, hit.position+ new Vector3(0, 0.3f, 0), Quaternion.identity, GrenadeParent.transform);

                    // Update counters
                    currentGunIndex = (currentGunIndex + 1) % grenadePrefabs.Length; // Move to the next gun index in a circular manner
                    totalSpawnedCount++;
                }
            }
        }
    }
}
