using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelProgression : MonoBehaviour
{
    // Start is called before the first frame update

    
    public LevelData nextLevelData;
 
    void Start()
    {
       // GameManager.Instance.nextLevelButton.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    

    public void KillBigEnemy()
    {

        int noOfBigEnemies = --GameManager.Instance.numOfBigEnemy;
        Debug.Log("no of big enemies" + noOfBigEnemies);
        if(noOfBigEnemies <= 0)
        {
            GameManager.Instance.ShowNextLevelButton();
            GameManager.Instance.SetLevelData(nextLevelData);
            
        }
    }

    public void NextLevelLoad()
    {
        
        Time.timeScale = 1f;
        GameManager.Instance.CurrentLevel = 1;
     
        SceneManager.LoadScene(GameManager.Instance.CurrentLevel);
        

    }
    private IEnumerator LoadGameScene()
    {
        // Load the game scene
        Time.timeScale = 1f;

        // Wait for the scene to finish loading
        yield return new WaitForSeconds(4f);


        Debug.Log("code coming here");
        if(DataPersistentManager.Instance != null)
        {
            Debug.Log("caling data persistent");
            DataPersistentManager.Instance.Reset();
        }
        else
        {
            Debug.Log("data persitent null");
        }
       
        /*if (GameManager.Instance != null)
        {
            Debug.Log("calling reset");
            GameManager.Instance.Reset();
            

        }
        else
        {
            Debug.Log("no gamemanager instance found");
        }*/
        
    }
}
