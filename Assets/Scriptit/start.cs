using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    

    public void PlayGame()
    {
        
        // Loads the next scene in the build index
        SceneManager.LoadScene("Lobby");
    }

    public void PlayAgain()
    {
        // Loads the scene named "Level1"
        SceneManager.LoadScene("Lobby");
    }

    public void QuitGame()
    {
        // Quits the application
        Application.Quit();
    }
}
