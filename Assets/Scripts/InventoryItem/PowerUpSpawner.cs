using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Array of item prefabs to spawn
    public int itemsToSpawn = 10; // Number of items to spawn for each prefab
    public float spawnRadius = 10f; // Radius within which items will be spawned
    public LayerMask groundLayer; // Layer mask for the ground or NavMesh area

    void Start()
    {
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems()
    {
        NavMeshHit hit;
        //Debug.Log("lengtyh" + itemPrefabs.Length);
        for (int i = 0; i < itemPrefabs.Length; i++)
        {
           // Debug.Log("spawning.....3");
            for (int j = 0; j < itemsToSpawn; j++)
            {

                Vector3 randomPoint = transform.position + Random.insideUnitSphere * spawnRadius + new Vector3(0,3f,0);
                //Debug.Log("spawning.....2" + randomPoint);
                if (GetRandomNavMeshPosition(out randomPoint))
                {
                    //Debug.Log("spawning.....");

                    GameObject item = Instantiate(itemPrefabs[i], randomPoint, Quaternion.identity, transform);

                    yield return null; // Wait for one frame to allow NavMesh to update
                }
                else
                {
                    // Handle if the random point is not on the NavMesh
                    Debug.LogWarning("Failed to spawn item at random point: " + randomPoint);
                }
            }
        }


        
    }
    /*private void SpawnEnemies()
    {
        // Generate enemies within the NavMesh area
        Debug.Log("No of enemies spawn + " + numOfEnemies);
        for (int i = 0; i < numOfEnemies; i++) // Spawn 100 enemies
        {
            // Sample a random position within the NavMesh area
            Vector3 randomPosition;
            if (GetRandomNavMeshPosition(out randomPosition))
            {
                // Spawn enemy at the sampled position
                GameObject newEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
                if (enemiesParent != null)
                {
                    newEnemy.transform.parent = enemiesParent.transform;
                }
            }
        }
    }*/

    private bool GetRandomNavMeshPosition(out Vector3 randomPosition)
    {
        randomPosition = Vector3.zero;
        /*NavMeshHit hit;
        if (NavMesh.SamplePosition(Vector3.zero, out hit, 1000000f, NavMesh.AllAreas))
        {

            randomPosition = hit.position;
            Debug.Log("generating random position"+randomPosition);
            return true;
        }*/
        NavMeshHit hit;
        Vector3 randomDestination = Random.insideUnitSphere * 200f + transform.position;
        NavMesh.SamplePosition(randomDestination, out hit, 10000f, NavMesh.AllAreas);
        //Debug.Log("new position ->" + hit.position);
        randomPosition = hit.position;
        return true;
    }
}
