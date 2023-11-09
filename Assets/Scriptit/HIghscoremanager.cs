using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public Text[] highScoreTexts; // Assign these in the Inspector
    private string[] highScoreNames;
    private string[] highScoreTimes;

    private void Start()
    {
        LoadHighScores();
        DisplayTopThreeScores();
    }

    private void LoadHighScores()
    {
        // Load the high scores from PlayerPrefs
        highScoreNames = new string[3];
        highScoreTimes = new string[3];

        for (int i = 0; i < 3; i++)
        {
            highScoreNames[i] = PlayerPrefs.GetString("HighScoreName" + (i + 1), "None");
            highScoreTimes[i] = PlayerPrefs.GetString("HighScoreTime" + (i + 1), "00:00:00");
        }
    }

    private void DisplayTopThreeScores()
    {
        for (int i = 0; i < 3; i++)
        {
            if (highScoreTexts[i] != null)
            {
                highScoreTexts[i].text = highScoreNames[i] + " - " + highScoreTimes[i];
            }
            else
            {
                Debug.LogError("HighScoreText at index " + i + " is not assigned in the Inspector.");
            }
        }
    }

    // Call this method whenever there's a change in the high scores during runtime
    public void UpdateHighScores()
    {
        LoadHighScores();
        DisplayTopThreeScores();
    }
}
