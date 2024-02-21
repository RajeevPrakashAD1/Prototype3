using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    // Method to start the game by loading a scene
    public void StartGame()
    {
        Debug.Log("Starting game...");
        SceneManager.LoadScene(1);
    }
}
