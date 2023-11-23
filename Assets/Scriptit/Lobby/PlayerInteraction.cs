using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public LayerMask interactableLayer;
    public TextMeshProUGUI interactionText;

    private GameObject currentInteractable;
    private bool isPressingE = false;
    private GameObject heldItem;

    void Update()
    {
        CheckForInteractable();
        HandleInput();

        // Tee tietty toiminto, kun pelaaja painaa "E" painiketta objektiin jolla on tietty tag.
        if (currentInteractable != null)
        {
            if (currentInteractable.CompareTag("Car"))
            {
                interactionText.text = "Press [E] to start the game";
                if (isPressingE && Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene("Level1");
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
            else if (currentInteractable.CompareTag("Item"))
            {
                interactionText.text = "Press [E] to hold";
                if (isPressingE)
                {
                    HoldItem(currentInteractable);
                }
            }
            // Voi lisätä muita toimintoja
        }
        else
        {
            interactionText.text = ""; // Pyyhi interaction teksti
        }

        // Pudota esine kuin vapautat "E" painikkeen
        if (!isPressingE && heldItem != null)
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

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isPressingE = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            isPressingE = false;
        }
    }

    void HoldItem(GameObject item)
    {
        // Lisää esine pelaajan lapseksi
        heldItem = item;
        heldItem.transform.parent = transform;
        heldItem.GetComponent<Rigidbody>().isKinematic = true; // Poista physiikka käytöstä esineestä
    }

    void DropItem()
    {
        // Poista esine pelaajalta
        if (heldItem != null)
        {
            heldItem.transform.parent = null;
            heldItem.GetComponent<Rigidbody>().isKinematic = false; // Ota physiikka käyttöön esineeseen
            heldItem = null;
        }
    }
}