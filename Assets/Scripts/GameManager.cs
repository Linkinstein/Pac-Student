using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject scoreboard;
    public GameObject timer;
    public TextMesh text;
    public Tilemap tiles;

    private float startTime;

    void Start()
    {
        StartCoroutine(Countdown());
        startTime = Time.time;
    }

    void Update()
    {
        if (IsTilemapEmpty())
        {
            int highScore = scoreboard.GetComponent<HighScoreTracker>().score;
            float bestTime = Time.time - startTime - 24.0f;
            if (PlayerPrefs.GetInt("HighScore", 0) < highScore)
            {
                PlayerPrefs.SetInt("HighScore", highScore);
                PlayerPrefs.SetFloat("BestTime", bestTime);
            }
            else if (PlayerPrefs.GetFloat("BestTime", 0f) < bestTime && PlayerPrefs.GetInt("HighScore", 0) == highScore)
            {
                PlayerPrefs.SetFloat("BestTime", bestTime);
            }
            SceneManager.LoadScene("StartScene");
        }
    }



    IEnumerator Countdown()
    {
        text.characterSize = 0.13f;
        text.text = "THEY ARE\nCOMING";
        yield return new WaitForSeconds(10f);
        text.text = "ARE YOU\nREADY?";
        yield return new WaitForSeconds(10f);
        text.characterSize = 0.7f;
        text.text = "3";
        yield return new WaitForSeconds(1);
        text.text = "2";
        yield return new WaitForSeconds(1);
        text.text = "1";
        yield return new WaitForSeconds(1);
        text.text = "GO";
        yield return new WaitForSeconds(1);
        text.text = "";
        player.GetComponent<PacStudentController>().started = true;
        timer.GetComponent<Timer>().ToggleTimer();
    }

    bool IsTilemapEmpty()
    {
        BoundsInt bounds = tiles.cellBounds;
        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            TileBase tile = tiles.GetTile(position);
            if (tile != null)
            {
                return false;
            }
        }
        return true;
    }

    public void died()
    { 
        
    }
}