using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Needed for IEnumerator

public class CheckpointTrigger : MonoBehaviour
{
    public string nextSceneName; // The name of the scene you want to load after reaching the checkpoint
    public float delayInSeconds = 2f; // The delay in seconds before the scene loads

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the player is the one triggering the checkpoint
        {
            StartCoroutine(WaitAndLoadScene(delayInSeconds));
        }
    }

    private IEnumerator WaitAndLoadScene(float delay)
    {
        // First we wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Then we check if the Timer component exists in the GameObject
        Timer timerInstance = GetComponent<Timer>();
        if (timerInstance == null)
        {
            // If it doesn't exist, we add it to the GameObject
            timerInstance = gameObject.AddComponent<Timer>();
        }

        // Then we load the scene
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);

            // Assuming Timer has a method to stop it, we call it here
            timerInstance.StopTimer();
        }
        else
        {
            Debug.LogError("Next scene name is not set!");
        }
    }
}
