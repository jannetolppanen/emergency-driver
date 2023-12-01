using UnityEngine;
using TMPro;

// Timer class inherits from MonoBehaviour
public class Timer : MonoBehaviour
{
    
    public TMP_Text TimerText; // TextMeshPro text component for displaying the timer
    private float startTime; // The time when the timer started
    private bool isTimerRunning = false; // Flag to check if the timer is running
    private bool isPlayerOnTerrain = false; // Flag to check if the player is on the terrain
    private float terrainEntryTime;
    public float delay = 2f; // Delay period in seconds

    // Property to get the last recorded time
    public static string LastRecordedTime { get; private set; }

    // Method called when the script instance is being loaded
    void OnEnable()
    {
        StartTimer();
    }

    // Method called when the behaviour becomes disabled or inactive
    void OnDisable()
    {
        isTimerRunning = false;
        float t = GetElapsedTime();
        LastRecordedTime = FormatTime(t);
        Debug.Log("Player Time: " + LastRecordedTime);
    }

    // Method to start the timer
    private void StartTimer()
    {
        if (!isTimerRunning)
        {
            startTime = Time.time;
            isTimerRunning = true;
        }
    }

    // Method called every frame
    private void Update()
    {
        if (isTimerRunning)
        {
            UpdateTimerDisplay();
            // Check if the player is on the terrain and has been for more than 2 seconds
            if (isPlayerOnTerrain && Time.time - terrainEntryTime >= delay)
            {
                // Decrease the start time by 60 seconds
                startTime -= 60f;
              
                // Reset the terrain entry time
                terrainEntryTime = Time.time;
            }
        }
    }


    

    // Method to update the timer display
    private void UpdateTimerDisplay()
    {
        float t = GetElapsedTime();
        LastRecordedTime = FormatTime(t);

        if (TimerText != null)
        {
            TimerText.text = "Time: " + LastRecordedTime;
        }
    }

    // Method to format the time
    private string FormatTime(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);
        int milliseconds = Mathf.FloorToInt((t * 100) % 100);

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    // Method to get the elapsed time
    private float GetElapsedTime()
    {
        float elapsedTime = Time.time - startTime;

        return elapsedTime;
    }

    // Method to set if the player is on the terrain

    public void SetPlayerOnTerrain(bool onTerrain)
    {
        if (isPlayerOnTerrain != onTerrain)
        {
            isPlayerOnTerrain = onTerrain;

            if (isPlayerOnTerrain)
            {
                // Record the time the player entered the terrain
                terrainEntryTime = Time.time;
            }
         
        }
    }

    // Method to convert formatted time to seconds
    public static float ConvertFormattedTimeToSeconds(string timeString)
    {
        if (string.IsNullOrEmpty(timeString))
        {
            Debug.LogError("Time string is null or empty.");
            return 0f;
        }

        string[] timeComponents = timeString.Split(':');

        if (timeComponents.Length != 3)
        {
            Debug.LogError("Invalid time format.");
            return 0f;
        }

        int minutes, seconds, milliseconds;
        if (!int.TryParse(timeComponents[0], out minutes) ||
            !int.TryParse(timeComponents[1], out seconds) ||
            !int.TryParse(timeComponents[2], out milliseconds))
        {
            Debug.LogError("Failed to parse time components to integers.");
            return 0f;
        }

        return minutes * 60f + seconds + milliseconds / 100f;
    }
}
