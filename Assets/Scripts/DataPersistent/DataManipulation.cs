
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DataManipulation : MonoBehaviour, IDataPersistence
{
    public Text enemyKillCountText;
    public Text highestEnemyKillCountText;
    public Text fastestTimeText;
    public Text yourTimeText;
    public Text coinsEarnedThisGameText;
    public Text totalCoinsText;

    private int enemyKillCount;
    private int highestEnemyKill;
    private float fastestTime;
    private float yourTime;
    private int coinsEarnedThisGame;
    private int totalCoins;

    void Start()
    {
 
    }
    public void Update()
    {
        
    }
    // Method to update text values
     private void setValues()
    {
        //Debug.Log("set values ....");
        enemyKillCount = GameManager.Instance.numofEnemyKill;
        highestEnemyKill = Mathf.Max(enemyKillCount, highestEnemyKill);
        if(GameManager.Instance.numOfBigEnemy <= 0)
        {
            yourTime = GameManager.Instance.elapsedTime;
            fastestTime = Mathf.Min(yourTime, fastestTime);
            coinsEarnedThisGame = 100 * (1 / (int)(yourTime + 1)) + 200 * enemyKillCount;
        }
        else
        {
            coinsEarnedThisGame = 50;
        }
        
      
        
        
        totalCoins = coinsEarnedThisGame + totalCoins;
        Debug.Log(coinsEarnedThisGame + " " + totalCoins + " " + totalCoins);
    }
    public void UpdateText()
    {
        setValues();
        
        enemyKillCountText.text = "Enemy Kill Count: " + enemyKillCount;
        highestEnemyKillCountText.text = "Highest Kill: " + highestEnemyKill.ToString();
        fastestTimeText.text = "Fastest Time: " + FormatTime(fastestTime);
        if (yourTime > 0) { yourTimeText.text = "Your Time: " + FormatTime(fastestTime); }
        else { yourTimeText.text = "Your Time: " + "Not Completed"; }
        
        coinsEarnedThisGameText.text = "Coins Earned: " + coinsEarnedThisGame.ToString();
        totalCoinsText.text = "Total Coins: " + totalCoins.ToString();
    }

    // Method to format time (convert seconds to mm:ss format)
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void LoadData(GameData data)
    {
        // throw new System.NotImplementedException();
        highestEnemyKill = data.KillCount;
        fastestTime = data.timeToComplete;
        
        totalCoins = data.coins;
        Debug.Log("loading data"+ highestEnemyKill + " " + fastestTime + " " + totalCoins);
    }

    public void SaveData(ref GameData data)
    {
        // throw new System.NotImplementedException();
       // Debug.Log("saving data in data manupulation");
        data.KillCount = highestEnemyKill;
        data.coins = totalCoins;
        data.timeToComplete = fastestTime;
    }
}
