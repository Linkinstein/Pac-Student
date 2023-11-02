using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public TextMesh text;

    void Start()
    {
        Debug.Log("STARTING");
        StartCoroutine(Countdown());
    }

    void Update()
    {

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
    }
}