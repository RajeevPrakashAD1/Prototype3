using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "ScriptableObjects/Bullet")]
public class BulletData : ScriptableObject
{
    public float speed = 20f;
    public float damage = 10f;
    public float ttl = 3f;
    public GameObject impactEffect;
    public Sprite img;
}
