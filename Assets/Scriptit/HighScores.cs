using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    public Text highScoreText; // Assign this in the Inspector

    private string playerName;
    private string elapsedTime;

    private void Start()
    {
        // Retrieve the player's name
        playerName = PlayerNameInput.playerName;

        // Retrieve the elapsed time from the static variable in Timer
        elapsedTime = Timer.LastRecordedTime;

        // Display the player's name and time in the UI
        DisplayHighScore(playerName, elapsedTime);

        // Save the player's name and time for high score purposes
        SaveHighScore(playerName, elapsedTime);
    }

    private void SaveHighScore(string name, string time)
    {
        // Save the player's name and time using PlayerPrefs
        PlayerPrefs.SetString("HighScoreName", name);
        PlayerPrefs.SetString("HighScoreTime", time);
    }

    private void DisplayHighScore(string name, string time)
    {
        // Check if the Text object is assigned
        if (highScoreText != null)
        {
            // Update the Text object to show the player's name and time
            highScoreText.text = name + " - " + time;
        }
        else
        {
            // Log an error if the Text object isn't assigned
            Debug.LogError("HighScoreText not assigned in the Inspector.");
        }
    }
}
