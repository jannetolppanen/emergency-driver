using UnityEngine;

public class Terraincollision : MonoBehaviour
{
    public Timer timer; // Reference to your Timer script

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Car is on the terrain!");
            // Add any actions or behaviors you want to perform when the car is on the terrain

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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Car left the terrain!");
            // Add any actions or behaviors you want to perform when the car leaves the terrain

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
