using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    // Method to start the game by loading a scene
    public void StartGame()
    {
        Debug.Log("Starting game...");
       // StartCoroutine(LoadGameScene());
        SceneManager.LoadScene(1);
       
        
    }

    private IEnumerator LoadGameScene()
    {
        // Load the game scene

        
        // Wait for the scene to finish loading
        yield return new WaitForSeconds(4f);
       

        // Access the GameManager instance and call its Reset method
        Debug.Log("accessing something");
        if (GameManager.Instance != null)
        {
            Debug.Log("calling reset");
           GameManager.Instance.Reset();

        }
        else
        {
            Debug.Log("no instance found");
        }
        playerInput.enabled = false;
        playerInput.enabled = true;
    }
}
