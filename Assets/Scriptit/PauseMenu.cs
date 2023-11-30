using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;

public class PauseMenu : MonoBehaviour
{
    public Canvas menu;

    // Start is called before the first frame update
    void Start()
    {
        menu = GetComponent<Canvas>();
        menu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            menu.enabled = true;
            Time.timeScale = 0;
            AudioListener.pause = true;
        }

    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        menu.enabled = false;
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
