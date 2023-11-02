using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostTimer : MonoBehaviour
{
    public Text text;

    public void Start()
    {
        GetComponent<Image>().enabled = false;
    }

    public void Tick()
    {
        if (!ticking)
        {
            StartCoroutine(ScaredToLoop());
        }
    }

    bool ticking = false;

    IEnumerator ScaredToLoop()
    {
        ticking = true;
        GetComponent<Image>().enabled = true;
        text.text = "10";
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1);
            text.text = (9-i).ToString();
        }
        yield return new WaitForSeconds(1);
        text.text = " ";
        ticking = false;
        GetComponent<Image>().enabled = false;
    }
}
