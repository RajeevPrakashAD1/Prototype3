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
   
   
}
