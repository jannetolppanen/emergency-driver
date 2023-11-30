using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseHud;
    private bool isPaused = false;


    void Start()
    {
        pauseHud.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    void PauseGame()
    {
        isPaused = true;
        pauseHud.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseHud.SetActive(false);
        AudioListener.pause = false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioListener.pause = false;
    }

    public void QuitLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        AudioListener.pause = false;
    }
}
