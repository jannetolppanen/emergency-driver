using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Reference to the Text component for displaying the timer
    public Text timerText;

    // The time when the timer starts
    private float startTime;

    // Indicates if the timer is currently running
    private bool isTimerRunning = false;

    // Static property to access the time from other scenes or scripts
    public static string LastRecordedTime { get; private set; }

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
        // Calculate the elapsed time
        float t = Time.time - startTime;

        // Format the minutes and seconds
        string minutes = ((int)t / 60).ToString("00");
        string seconds = (t % 60).ToString("00");

        // Format the time string
        string timeString = string.Format("{0}:{1}", minutes, seconds);

        // Update the timer text if it is assigned
        if (timerText != null)
        {
            timerText.text = "Time: " + timeString;
        }
        else
        {
            // Log a warning if the timer text is not assigned
            Debug.LogWarning("timerText is not assigned in the Timer script.");
        }
    }

    // Method to get the current time as a formatted string
    public string GetTime()
    {
        float t = Time.time - startTime;

        // Format the minutes and seconds
        string minutes = ((int)t / 60).ToString("00");
        string seconds = (t % 60).ToString("00");

        // Format the time string
        return string.Format("{0}:{1}", minutes, seconds);
    }

    // Method to stop the timer
    public void StopTimer()
    {
        // Only stop the timer if it is running
        if (isTimerRunning)
        {
            isTimerRunning = false;

            // Record the last recorded time
            LastRecordedTime = GetTime();
            Debug.Log("Player Time: " + LastRecordedTime);
        }
    }
}
