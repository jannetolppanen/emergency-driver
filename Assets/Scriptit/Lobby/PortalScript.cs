using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public string sceneToLoad = "YourSceneName";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the portal. Loading scene...");
            SceneManager.LoadScene(sceneToLoad);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
