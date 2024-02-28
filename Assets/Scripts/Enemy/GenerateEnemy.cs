
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
    private float numOfEnemies;
        

    private void Start()
    {
       
        numOfEnemies = GameManager.Instance.levelData.numberOfSmallEnemies;
        SpawnEnemies();
    }

    private void SpawnEnemies()
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
        //Debug.Log("new position ->" + hit.position);
        randomPosition = hit.position;
        return true;
    }
}
