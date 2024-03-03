using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    //public Level
    public PickGun pickGun;
    

    private void Start()
    {
        //Debug.Log("accessing something");
        if (GameManager.Instance != null)
        {
            //Debug.Log("calling reset");
            GameManager.Instance.Reset();
        }
        else
        {
            Debug.Log("no instance found");
        }
    }

    // Load a level by name and pass level data to the GameManager
  

    // Other LevelManager methods...
}
