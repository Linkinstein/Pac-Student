using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip startBGM;
    public AudioClip loopBGM;

    void Start()
    {
        StartCoroutine(playEngineSound());
    }

    void Update()
    {

    }

    IEnumerator playEngineSound()
    {
        GetComponent<AudioSource>().clip = startBGM;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        GetComponent<AudioSource>().clip = loopBGM;
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();
    }
}
