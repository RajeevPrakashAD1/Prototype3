using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class LevelManager : MonoBehaviour
{

    //public Level
    public PickGun pickGun;

    public PlayerInput playerInput;

    private void Start()
    {

        playerInput = GetComponent<PlayerInput>();
        //Debug.Log("accessing something");
        if (GameManager.Instance != null)
        {
            
            
            GameManager.Instance.Reset();
        }
        if(DataPersistentManager.Instance != null)
        {
            DataPersistentManager.Instance.Reset();
        }
        playerInput.enabled = false;
        playerInput.enabled = true;
    }

    // Load a level by name and pass level data to the GameManager
  

    // Other LevelManager methods...
}
