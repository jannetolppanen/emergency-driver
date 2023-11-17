using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    // Reference to the Text component for displaying the timer
    public TMP_Text TimerText;

    // The time when the timer starts
    private float startTime;

    // Indicates if the timer is currently running
    private bool isTimerRunning = false;
    // indicates if the player is on terrain
    private bool isPlayerOnTerrain = false;

    // Static property to access the time from other scenes or scripts
    public static string LastRecordedTime { get; private set; }
    public static float LastRecordedTimeInSeconds { get; private set; }



    // Called when the GameObject becomes enabled and active
    void OnEnable()
    {
        // Start the timer when the scene is loaded or GameObject is enabled
        StartTimer();
    }

    // Called when the GameObject becomes disabled or inactive
    void OnDisable()
    {
        // Stop the timer when the scene is unloaded or GameObject is disabled
        StopTimer();
    }

    // Method to start the timer
    public void StartTimer()
    {
        // Check if the timer isn't already running
        if (!isTimerRunning)
        {
            startTime = Time.time;
            isTimerRunning = true;
        }
    }


    // Called every frame
    private void Update()
    {
        // Update the timer display if the timer is running
        if (isTimerRunning)
        {
            UpdateTimerDisplay();
        }
    }

    // Method to update the display of the timer
    private void UpdateTimerDisplay()
    {
        // Calculate the elapsed time in seconds including microseconds
        float t = Time.time - startTime;
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);
        int milliseconds = Mathf.FloorToInt((t * 100) % 100); // Show only first two digits of milliseconds

        // Format the time string including minutes, seconds, and milliseconds
        string timeString = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);

        // Update the timer text if it is assigned
        if (TimerText != null)
        {
            TimerText.text = "Time: " + timeString;
        }
    }



    // Method to calculate the total time in seconds
    private float CalculateTimeInSeconds()
    {
        float t = Time.time - startTime;
        int minutes = (int)t / 60;
        int seconds = (int)t % 60;
        return (minutes * 60) + seconds;
    }
    // Method to convert formatted time string to seconds
    public static float ConvertFormattedTimeToSeconds(string timeString)
    {
        if (string.IsNullOrEmpty(timeString))
        {
            Debug.LogError("Time string is null or empty.");
            return 0f; // or handle the error accordingly
        }

        // Split the time string into its components (minutes, seconds, milliseconds)
        string[] timeComponents = timeString.Split(':');

        if (timeComponents.Length != 3)
        {
            Debug.LogError("Invalid time format.");
            return 0f; // or handle the error accordingly
        }

        int minutes, seconds, milliseconds;
        if (!int.TryParse(timeComponents[0], out minutes) ||
            !int.TryParse(timeComponents[1], out seconds) ||
            !int.TryParse(timeComponents[2], out milliseconds))
        {
            Debug.LogError("Failed to parse time components to integers.");
            return 0f; // or handle the error accordingly
        }

        // Calculate total time in seconds
        float totalTimeInSeconds = minutes * 60f + seconds + milliseconds / 100f;
        return totalTimeInSeconds;
    }

    // Method to get the current time as a formatted string
    public string GetTime()
    {
        float t = Time.time - startTime;

        // Calculate minutes, seconds, and milliseconds
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);
        int milliseconds = Mathf.FloorToInt((t * 100f) % 100f);

        // Format the time string
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }


   
    public void SetPlayerOnTerrain(bool onTerrain)
    {
        if (isPlayerOnTerrain != onTerrain)
        {
            isPlayerOnTerrain = onTerrain;

            if (isPlayerOnTerrain)
            {
                Debug.Log(startTime);
                startTime -= 60f;
               
            }
         

        }
    }


    // Method to stop the timer
    public void StopTimer()
    {
        // Only stop the timer if it is running
        if (isTimerRunning)
        {
            isTimerRunning = false;

            // Record the last recorded time as a formatted string ("mm:ss")
            LastRecordedTime = GetTime();
            Debug.Log("Player Time: " + LastRecordedTime);

            // Calculate and store the last recorded time in seconds
            LastRecordedTimeInSeconds = CalculateTimeInSeconds();
        }
    }
  
}
