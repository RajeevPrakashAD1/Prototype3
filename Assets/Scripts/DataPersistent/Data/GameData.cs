using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // Start is called before the first frame update
    public int KillCount;
    public float timeToComplete;
    public int coins;
    public GameData()
    {
        this.KillCount = 0;
        this.timeToComplete = 3600f;
        this.coins = 0;
    }
   
}
