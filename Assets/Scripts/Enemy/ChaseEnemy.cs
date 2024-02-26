using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public float chaseRadius = 200f; // Radius within which enemies start chasing the player
    //public LayerMask playerLayer;   // Layer mask to identify the player

    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component
    private Transform player;          // Reference to the player's transform
    private bool isChasing = false;    // Flag to track whether enemy is currently chasing the player

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming player is tagged as "Player"
    }

    void Update()
    {
        // Check if the player is within the chase radius
        if (Vector3.Distance(transform.position, player.position) <= chaseRadius)
        {
            // Start chasing the player
           // Debug.Log("making chasing true");
            isChasing = true;

            // Set the destination to the player's position
            navMeshAgent.SetDestination(player.position);
        }
        else
        {
            // Stop chasing if the player is outside the chase radius
            isChasing = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the chase radius in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
