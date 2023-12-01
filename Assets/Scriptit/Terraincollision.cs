using UnityEngine;
using System.Collections;

public class Terraincollision : MonoBehaviour
{
    public Timer timer;
    private float enterTime;
    public float cooldown = 2f;
    public float exitCooldown = 1f;
    public GameObject redAlert;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is on the terrain!");
            enterTime = Time.time;

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
            Debug.Log("Player left the terrain!");

            if (timer != null)
            {
                timer.SetPlayerOnTerrain(false);
            }
            else
            {
                Debug.LogWarning("Timer reference not assigned!");
            }

            StartCoroutine(DeactivateRedAlertAfterDelay());
        }
    }

    IEnumerator DeactivateRedAlertAfterDelay()
    {
        yield return new WaitForSeconds(exitCooldown);
        redAlert.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time - enterTime >= cooldown)
            {
                redAlert.SetActive(true);

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
}
