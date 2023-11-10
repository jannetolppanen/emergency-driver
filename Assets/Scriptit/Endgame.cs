using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class CheckpointTrigger : MonoBehaviour
{
    // The name of the scene you want to load after reaching the checkpoint
    public string nextSceneName;

    // The delay in seconds before the scene loads
    public float delayInSeconds = 2f;

    // Called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Start a coroutine to wait for a specified delay and then load the next scene
            StartCoroutine(WaitAndLoadScene(delayInSeconds));
        }
    }

    // Coroutine to wait for a specified delay and then load the next scene
    private IEnumerator WaitAndLoadScene(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Check if the next scene name is not empty or null
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            // Load the specified scene
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // Log an error if the next scene name is not set
            Debug.LogError("Next scene name is not set!");
        }
    }
}
