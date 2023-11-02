using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersUpport : MonoBehaviour
{
    public GameObject ghost1;
    public GameObject ghost2;
    public GameObject ghost3;
    public GameObject ghost4;
    public AudioSource audio;
    public AudioClip clip;

    private void OnDestroy()
    {
        ghost1.GetComponent<Animator>().SetBool("Scared", true);
        ghost2.GetComponent<Animator>().SetBool("Scared", true);
        ghost3.GetComponent<Animator>().SetBool("Scared", true);
        ghost4.GetComponent<Animator>().SetBool("Scared", true);
        audio.clip = clip;
        audio.Play();
    }
}
