using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float startTime;
    private bool isTimerRunning = false;

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
            }
            else
            {
                Debug.LogWarning("timerText is not assigned in the Timer script.");
            }
        }
    }


    public void StopTimer()
    {
        isTimerRunning = false;
    }
}
