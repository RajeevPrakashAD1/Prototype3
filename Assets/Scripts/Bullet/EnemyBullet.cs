using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed = 20f; // Speed of the bullet
    private Vector3 targetPosition; // Position to travel towards
    private bool hasHitPlayer = false; // Flag to track if the bullet has hit the player

    // Method to set the target position for the bullet
    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
    }

    private void Update()
    {
        // Move the bullet towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the bullet has reached the target position
        if ( Vector3.Distance(transform.position , targetPosition) <0.1f)
        {
            // If the bullet has hit the player, log it
           /* if (hasHitPlayer)
            {
                Debug.Log("Player hit!");
            }*/
            // Destroy the bullet regardless of hitting the player or not
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Check if the bullet has collided with the player
        //Debug.Log("enemy bullet collided" + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            //hasHitPlayer = true;
            GameManager.Instance.DamagePlayer(20);
            Destroy(gameObject);

        }
        
        
    }

}
