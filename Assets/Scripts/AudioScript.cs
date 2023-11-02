using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip startBGM;
    public AudioClip loopBGM;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(playEngineSound());
    }

    void Update()
    {

    }

    IEnumerator playEngineSound()
    {
        audioSource.clip = startBGM;
        audioSource.Play();
        Debug.Log(GetComponent<AudioSource>().clip.length);
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        audioSource.clip = loopBGM;
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }
}
