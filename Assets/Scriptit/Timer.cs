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




    // Method to stop the timer
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
