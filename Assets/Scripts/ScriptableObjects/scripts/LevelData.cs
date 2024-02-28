using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public int numberOfBigEnemies; // Number of big enemies to spawn
    public int numberOfSmallEnemies; // Number of small enemies to spawn
    public float bigEnemySpeed; // Speed of big enemies
    public float smallEnemySpeed; // Speed of small enemies
    public float bigEnemyFireRate; // Fire rate of big enemies
    public float smallEnemyFireRate; // Fire rate of small enemies
    public float smallEnemyDamage;
    public float bigEnemyDamage;
}
