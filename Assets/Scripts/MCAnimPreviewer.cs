using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCAnimPreviewer : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    public int dir;
    [SerializeField]
    public bool dead;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Direction", dir);
        animator.SetBool("Dead", dead);
    }
}
