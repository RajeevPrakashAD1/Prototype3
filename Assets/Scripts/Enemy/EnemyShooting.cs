using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float shootingInterval = 0.5f;
    public float shootingRange = 10f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("ShootAtPlayer", 0f, shootingInterval);
    }

    private void ShootAtPlayer()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) <= shootingRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, rotation);

            // Set the target position for the bullet
            bullet.GetComponent<EnemyBullet>().SetTargetPosition(player.position);
        }
    }
}
