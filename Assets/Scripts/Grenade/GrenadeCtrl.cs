using UnityEngine;

public class GrenadeCtrl : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab for explosion effect
    public float explosionDelay = 3f; // Delay before explosion
    public float explosionForce = 1000f; // Force of the explosion
    public float explosionRadius = 5f; // Radius of the explosion
    private bool hasExploded = false; // Flag to track if grenade has exploded

    private void Start()
    {
        // Start countdown for explosion
        Invoke("Explode", explosionDelay);
    }

    private void Explode()
    {
        // Check if the grenade has already exploded
        if (hasExploded)
            return;

        // Set flag to indicate that grenade has exploded
        hasExploded = true;

        // Instantiate explosion effect at grenade's position
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);

        // Find nearby colliders within explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            // Check if the nearby object has a Rigidbody component
           // Debug.Log("nearby object   "+nearbyObject.gameObject.tag);
            if(nearbyObject.gameObject.tag == "Enemy")
            {
                //Debug.Log("destrying..enemy");
                Destroy(nearbyObject.gameObject);

            }else if(nearbyObject.gameObject.tag == "Player")
            {
                GameManager.Instance.DamagePlayer(1000);
            }else if(nearbyObject.gameObject.tag == "BigEnemy")
            {
                BigEnemy be = nearbyObject.gameObject.GetComponent<BigEnemy>();
                be.SetHealth(400);
            }

            // Check if the nearby object has a destructible component (e.g., Health system)
           
        }

        // Destroy the grenade object
        Destroy(gameObject);
    }
}
