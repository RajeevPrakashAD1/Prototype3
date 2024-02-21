using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySurfaceAdjustment : MonoBehaviour
{
    // Start is called before the first frame update
    public Terrain terrain; // Reference to the terrain in the scene

    private void Start()
    {
        terrain = Terrain.activeTerrain;
        // Adjust the position of the enemy to stay on the terrain surface
        AdjustToTerrainSurface();

    }
    private void Update()
    {
        
    }

    public void AdjustToTerrainSurface()
    {
        if (terrain != null)
        {
            // Get the terrain height at the enemy's position
            float terrainHeight = terrain.SampleHeight(transform.position);
            float enemyHeight = GetEnemyHeight();

            // Adjust the enemy's position to stay on the terrain surface
            transform.position = new Vector3(transform.position.x, terrainHeight + enemyHeight/2f + 5f, transform.position.z);
           

        }
        else
        {
            Debug.LogWarning("Terrain reference not set. Surface adjustment not applied.");
        }
    }
    private float GetEnemyHeight()
    {
        // Get the height of the enemy's collider or mesh
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            return collider.bounds.size.y;
        }

        // If collider is not found, use a default value
        Debug.LogWarning("Collider not found on enemy. Using default height value.");
        return 2.0f; // Default height value
    }
}
