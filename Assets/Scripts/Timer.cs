using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool isRunning = false;

    private void Start()
    {
        timerText.text = "Time:\n00:00:00";
        startTime = Time.time;
    }

    private void Update()
    {
        if (isRunning)
        {
            float elapsedTime = Time.time - startTime - 24.0f;
            TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
            timerText.text = "Time:\n" + string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }
    }

    public void ToggleTimer()
    {
        isRunning = !isRunning;
    }
}
