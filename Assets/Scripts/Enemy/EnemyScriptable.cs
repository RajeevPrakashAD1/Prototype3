using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data", order = 51)]
public class EnemyData : ScriptableObject
{
    public string type;
    public int health;
    public float shootingRate;
    public float shootingInterval;
    public float shootingRange;
    public float speed;
    
}
