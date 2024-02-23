using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletParent;
    public Transform bulletSpawnPoint;
    
    public EnemyData enemyData;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bulletParent = GameObject.FindGameObjectWithTag("EnemyBulletParent");

        InvokeRepeating("ShootAtPlayer", 0f, enemyData.shootingInterval);
    }

    private void ShootAtPlayer()
    {
       
        if (player != null && Vector3.Distance(transform.position, player.position) <= enemyData.shootingRange)
        {
            //Debug.Log("enemy shooting");
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity, bulletParent.transform) ;
            //Debug.Log(bullet.transform.position);

            // Set the target position for the bullet
            bullet.GetComponent<EnemyBullet>().SetTargetPosition(player.position);
        }
    }
}
