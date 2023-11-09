using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SceneChangeHandler : MonoBehaviour
{
    private BackendHandler backendHandler;

    private void Start()
    {
        backendHandler = GetComponent<BackendHandler>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        backendHandler.FetchhighScoresJSONFile();
        backendHandler.FetchhighScoresJSON();
        // Call other methods you want to trigger when the scene changes

        // Call all other methods from BackendHandler
        backendHandler.PostGameResults();
        // Add calls to other methods from the BackendHandler script here

    

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
