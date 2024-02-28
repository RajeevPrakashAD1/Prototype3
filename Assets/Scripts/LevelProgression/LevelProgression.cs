using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelProgression : MonoBehaviour
{
    // Start is called before the first frame update
    float noOfBigEnemies;
    public GameObject nextLevelButton;
    public LevelData nextLevelData;
    private void Awake()
    {
        nextLevelButton = GameObject.FindGameObjectWithTag("NextLevelButton");
        nextLevelButton.SetActive(false);
    }
    void Start()
    {
        noOfBigEnemies = GameManager.Instance.levelData.numberOfBigEnemies;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void ShowNextLevelButton()
    {
        
        
        Time.timeScale = 0f;
        nextLevelButton.SetActive(true);
        GameManager.Instance.SetLevelData(nextLevelData);
        GameManager.Instance.Reset();
        
    }

    public void KillBigEnemy()
    {
        noOfBigEnemies--;
        Debug.Log("no of big enemies" + noOfBigEnemies);
        if(noOfBigEnemies <= 0)
        {
            ShowNextLevelButton();
        }
    }

    public void NextLevelLoad()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
}
