using UnityEngine;
using UnityEngine.UI;
using HighScores;
using System;

public class HSCollector : MonoBehaviour
{
    // Reference to a Text component for displaying high scores. Assign this in the Inspector.
    public Text highScoreText;

    // Player's name and elapsed time variables
    private string playerName;
    private string elapsedTime;

    // Called when the script is first initialized
    private void Start()
    {
        // Retrieve the player's name from the PlayerNameInput script
        playerName = PlayerNameInput.playerName;

        // Retrieve the elapsed time from the static variable in the Timer script
        elapsedTime = Timer.LastRecordedTime;

        // Create a new HighScore object and initialize it with player data
        HighScore highScore = new HighScore();
        highScore.playername = playerName;
        highScore.playtime = elapsedTime;

        // Convert time string (mm:ss) to float (total seconds) and set the score
    

        // Serialize the HighScore object to JSON format
        string json = JsonUtility.ToJson(highScore);
        // Here you can store the JSON data or process it as needed

        // Display the player's name and time in the UI
        DisplayHighScore(playerName, elapsedTime);

        // Save the player's name and time for high score purposes
        SaveHighScore(playerName, elapsedTime);
    }

    // Save the player's name and time using PlayerPrefs with unique keys
    private void SaveHighScore(string name, string time)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (!PlayerPrefs.HasKey("HighScoreName" + i))
            {
                PlayerPrefs.SetString("HighScoreName" + i, name);
                PlayerPrefs.SetString("HighScoreTime" + i, time);
                break;
            }
        }
    }

    // Display the player's name and time in the UI using the assigned Text component
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
