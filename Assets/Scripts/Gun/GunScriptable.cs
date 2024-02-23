using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunData : ScriptableObject
{
    public string gunName;
    public float damage;
    public float fireRate;
    public int magazineSize;
    public float reloadTime;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    // Add more properties as needed

    // Method to fire the gun
    public void Fire()
    {
        // Implement firing logic here
    }

    // Method to reload the gun
    public void Reload()
    {
        // Implement reloading logic here
    }
}
