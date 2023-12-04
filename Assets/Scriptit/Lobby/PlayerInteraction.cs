using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public LayerMask interactableLayer;
    public TextMeshProUGUI interactionText;

    private GameObject currentInteractable;
    private GameObject heldItem;

    void Update()
    {
        CheckForInteractable();

        if (currentInteractable != null)
        {
            if (currentInteractable.CompareTag("Car"))
            {
                interactionText.text = "Press [E] to start the game";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene("Level1");
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
            else if (currentInteractable.CompareTag("Item"))
            {
                interactionText.text = "Press [E] to hold";
                if (Input.GetKeyDown(KeyCode.E) && heldItem == null)
                {
                    HoldItem(currentInteractable);
                }
            }
        }
        else
        {
            interactionText.text = "";
        }

        if (Input.GetKeyUp(KeyCode.E) && heldItem != null)
        {
            DropItem();
        }
    }

    void CheckForInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactableLayer))
        {
            currentInteractable = hit.collider.gameObject;
        }
        else
        {
            currentInteractable = null;
        }
    }

    void HoldItem(GameObject item)
    {
        // Aseta esine pelaaja objectin lapseksi
        heldItem = item;
        heldItem.transform.parent = transform;
        heldItem.GetComponent<Rigidbody>().isKinematic = true;
    }

    void DropItem()
    {
        // Poista esine pelaaja objectista
        heldItem.transform.parent = null;
        heldItem.GetComponent<Rigidbody>().isKinematic = false;
        heldItem = null;
    }
}