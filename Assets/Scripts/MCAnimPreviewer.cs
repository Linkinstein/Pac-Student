using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCAnimPreviewer : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    public int dir = 0;
    [SerializeField]
    public bool dead = false;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Direction", dir);
        animator.SetFloat("Dead", dead);
    }
}
