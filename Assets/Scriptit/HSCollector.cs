using UnityEngine;
using UnityEngine.UI;
using HighScores;
using System; // Add this line to include the System namespace

public class HSCollector : MonoBehaviour
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

        // Copying data to HighScore object
        HighScore highScore = new HighScore();
        highScore.playername = playerName;
        highScore.playtime = elapsedTime;

        // Convert time string (mm:ss) to float (total seconds)
        float score;
        string[] timeComponents = elapsedTime.Split(':');
        if (timeComponents.Length == 2 && float.TryParse(timeComponents[0], out float minutes) && float.TryParse(timeComponents[1], out float seconds))
        {
            score = minutes * 60 + seconds;
            highScore.score = score;
        }
        else
        {
            Debug.LogError("Failed to parse time string to float. elapsedTime value: " + elapsedTime);
        }

        // Serializing the HighScore object
        string json = JsonUtility.ToJson(highScore);
        // Here you can store the JSON data or process it as needed

        // Display the player's name and time in the UI
        DisplayHighScore(playerName, elapsedTime);

        // Save the player's name and time for high score purposes
        SaveHighScore(playerName, elapsedTime);
    }

    private void SaveHighScore(string name, string time)
    {
        // Save the player's name and time using PlayerPrefs with unique keys
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
