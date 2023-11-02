using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip startBGM;
    public AudioClip loopBGM;
    public AudioClip scareBGM;
    public AudioClip deadBGM;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(playEngineSound());
    }

    void Update()
    {
        if (audioSource.clip == scareBGM && !scaring)
        {
            StartCoroutine(ScaredToLoop());
        }
    }

    bool scaring = false;

    IEnumerator ScaredToLoop()
    {
        scaring = true;
        yield return new WaitForSeconds(10);

        audioSource.Stop();
        audioSource.clip = loopBGM;
        audioSource.Play();

        scaring = false;
    }

    IEnumerator playEngineSound()
    {
        audioSource.clip = startBGM;
        audioSource.Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        audioSource.clip = loopBGM;
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }
}
