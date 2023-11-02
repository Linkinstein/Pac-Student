using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("Scared"))
        {
            StartCoroutine(ScareTimer());
        }
    }

    IEnumerator ScareTimer()
    {
        Debug.Log("Scared");
        yield return new WaitForSeconds(7);
        Debug.Log("recovering");
        anim.SetBool("Recovering", true);
        yield return new WaitForSeconds(3);
        Debug.Log("recover");
        anim.SetBool("Scared", false);
        anim.SetBool("Recovering", false);
    }
}
