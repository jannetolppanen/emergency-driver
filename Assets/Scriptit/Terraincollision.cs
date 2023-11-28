using UnityEngine;

public class Terraincollision : MonoBehaviour
{
    public Timer timer; // Reference to your Timer script
    private float lastExitTime;
    public float cooldown = 2f; // Cooldown period in seconds
    private float currentTime; // Added this line

    private void Update()
    {
        currentTime = Time.time; // Added this line
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Car is on the terrain!");

            // Check if enough time has passed since the last time the player exited the terrain
            if (currentTime - lastExitTime >= cooldown)
            {
                // Set the player on the terrain in the Timer script
                if (timer != null)
                {
                    timer.SetPlayerOnTerrain(true);
                }
                else
                {
                    Debug.LogWarning("Timer reference not assigned!");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Car left the terrain!");

            // Update the last exit time
            lastExitTime = currentTime; // Modified this line

            // Set the player not on the terrain in the Timer script
            if (timer != null)
            {
                timer.SetPlayerOnTerrain(false);
            }
            else
            {
                Debug.LogWarning("Timer reference not assigned!");
            }
        }
    }
}
