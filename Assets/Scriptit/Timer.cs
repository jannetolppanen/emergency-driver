using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool isTimerRunning = false;
    // Static property to access the time from other scenes or scripts
    public static string LastRecordedTime { get; private set; }

    private void Start()
    {
        startTime = Time.time;
        isTimerRunning = true;

    }
  

    private void Update()
    {
        if (isTimerRunning)
        {
            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00");

            string timeString = string.Format("{0}:{1}", minutes, seconds);
            

            if (timerText != null)
            {
                timerText.text = "Time: " + timeString;
                Debug.Log(timeString);
                
            }
            else
            {
                Debug.LogWarning("timerText is not assigned in the Timer script.");
            }
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
        isTimerRunning = false;
        LastRecordedTime = GetTime();
        Debug.Log("Player Time: " + LastRecordedTime);
    }
}
