using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    public InputField nameInputField;
    public static string playerName;

    public void SavePlayerName()
    {
        playerName = nameInputField.text;
        Debug.Log("Player name: " + playerName);

    }
}