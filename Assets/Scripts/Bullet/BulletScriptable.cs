using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet Data", order = 51)]
public class BulletData : ScriptableObject
{
    public float speed = 10f;
    public float damage = 10f;
    public GameObject impactEffect;
}
