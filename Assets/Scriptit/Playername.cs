using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    // Reference to the input field for the player's name
    public InputField nameInputField;

    // Static variable to store the player's name accessible across instances
    public static string playerName;

    // Method to save the player's name from the input field
    public void SavePlayerName()
    {
        // Retrieve the player's name from the input field and save it to the static variable
        playerName = nameInputField.text;

        // Log the player's name for debugging purposes
        Debug.Log("Player name: " + playerName);
    }
}
