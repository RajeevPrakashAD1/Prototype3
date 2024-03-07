using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    
    private int currentWaypointIndex = 0;   // Index of the current waypoint
    private NavMeshAgent navMeshAgent;      // Reference to the NavMeshAgent component
    private Vector3 destination;            // Destination for the enemy to move towards
    public EnemyData enemyData;
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = enemyData.speed;
      

    }

    void Update()
    {
        // Check if the enemy has reached the destination
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 5f)
        {
            // Generate a random destination on the NavMesh surface
            GenerateRandomDestination();
        }
    }

  
    void GenerateRandomDestination()
    {
        // Generate a random point on the NavMesh surface
        NavMeshHit hit;
        Vector3 randomDestination = Random.insideUnitSphere * 100f + transform.position;
        NavMesh.SamplePosition(randomDestination, out hit, 10000f, NavMesh.AllAreas);
       // Debug.Log("new position ->" + hit.position);

        // Set the destination to the random point
        navMeshAgent.SetDestination(hit.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
       // Debug.Log(gameObject.name + "  collided with " + collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
        {
          
           
             //Destroy(collision.gameObject);
            GameManager.Instance.DamagePlayer(500);
            Destroy(gameObject);
        }
    }
}
