using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject startObject;
    private TextMesh text;

    void Start()
    {
        Debug.Log("STARTING");
        text = startObject.GetComponent<TextMesh>();
        StartCoroutine(Countdown());
    }

    void Update()
    {

    }

    IEnumerator Countdown()
    {
        Debug.Log("Hello?");
        text.text = "3";
        yield return new WaitForSeconds(1);
        text.text = "2";
        yield return new WaitForSeconds(1);
        text.text = "1";
        yield return new WaitForSeconds(1);
        text.text = "GO";
        yield return new WaitForSeconds(1);
        text.text = ""; 
        Debug.Log("bye?");
    }
}