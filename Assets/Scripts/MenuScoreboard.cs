using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScoreboard : MonoBehaviour
{
    public Text scoreboard;
    public Text timer;

    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        scoreboard.text = "High Score:\n" + highScore.ToString();
        TimeSpan timeSpan = TimeSpan.FromSeconds(bestTime);
        timer.text = "Time:\n" + string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }
}
