using UnityEngine;

public class BigEnemy : MonoBehaviour
{
    public int maxHits = 15; // Maximum number of hits allowed before destruction
    private int currentHits = 0; // Current number of hits

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a player bullet
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            // Increment the hit count
            currentHits++;

            // Check if the maximum hits limit has been reached
            if (currentHits >= maxHits)
            {
                // Destroy the BigEnemy object
                Destroy(gameObject);
            }
        }
    }
}
