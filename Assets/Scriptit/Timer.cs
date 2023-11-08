using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool isTimerRunning = false; // Indicates if the timer is currently running
    // Static property to access the time from other scenes or scripts
    public static string LastRecordedTime { get; private set; }

    void OnEnable() // This will start the timer when the scene is loaded/GameObject is enabled
    {
        StartTimer();
    }

    void OnDisable() // This will stop the timer when the scene is unloaded/GameObject is disabled
    {
        StopTimer();
    }

    public void StartTimer()
    {
        if (!isTimerRunning) // Check if the timer isn't already running
        {
            startTime = Time.time;
            isTimerRunning = true;
        }
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString("00");
        string seconds = (t % 60).ToString("00");

        string timeString = string.Format("{0}:{1}", minutes, seconds);

        if (timerText != null)
        {
            timerText.text = "Time: " + timeString;
        }
        else
        {
            Debug.LogWarning("timerText is not assigned in the Timer script.");
        }
    }

    public string GetTime()
    {
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString("00");
        string seconds = (t % 60).ToString("00");

        return string.Format("{0}:{1}", minutes, seconds);
    }

    public void StopTimer()
    {
        if (isTimerRunning) // Only stop the timer if it is running
        {
            isTimerRunning = false;
            LastRecordedTime = GetTime();
            Debug.Log("Player Time: " + LastRecordedTime);
        }
    }
}
