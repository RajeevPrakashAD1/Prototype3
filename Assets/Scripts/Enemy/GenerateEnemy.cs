/*using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Terrain terrain;
    public GameObject enemiesParent; // Reference to the parent GameObject for enemies
    *//*public NavMesh navMeshSurface;*//*

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        // Get terrain size
        Vector3 terrainSize = terrain.terrainData.size;

        for (int i = 0; i < 10; i++) // Spawn 10 enemies
        {
            // Generate random position within terrain bounds
            Vector3 randomPosition = new Vector3(
                Random.Range(0f, terrainSize.x),
                0f,
                Random.Range(0f, terrainSize.z)
            );

            // Convert random position to world space
            Vector3 worldPosition = terrain.transform.position + randomPosition;

            // Get terrain height at the random position
            float terrainHeight = terrain.SampleHeight(worldPosition);

            // Set enemy position with terrain height
            Vector3 enemyPosition = new Vector3(worldPosition.x, terrainHeight, worldPosition.z);

            // Spawn enemy at the calculated position
            GameObject newEnemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);

            // Set the parent of the instantiated enemy to the enemiesParent GameObject
            if (enemiesParent != null)
            {
                newEnemy.transform.parent = enemiesParent.transform;
            }
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Terrain terrain;
    public GameObject enemiesParent;
    public float spawnRadius = 10f;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        // Generate enemies within the NavMesh area
        for (int i = 0; i < 100; i++) // Spawn 100 enemies
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
    }

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
        Debug.Log("new position ->" + hit.position);
        randomPosition = hit.position;
        return true;
    }
}
