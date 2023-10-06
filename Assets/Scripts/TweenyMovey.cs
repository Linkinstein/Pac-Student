using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenyMovey : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float startTime;
    private int dir = 6;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("Direction", dir);
        initialPosition = transform.position;
        SetNextTargetPosition();
        startTime = Time.time;
    }

    private void SetNextTargetPosition()
    {
        animator.SetInteger("Direction", dir);
        switch (dir)
        {
            case 6: // Right
                targetPosition = initialPosition + new Vector3(5.0f, 0, 0);
                dir = 9;
                break;
            case 9: // Up
                targetPosition = initialPosition + new Vector3(0, 4.0f, 0);
                dir = 4;
                break;
            case 4: // Left
                targetPosition = initialPosition + new Vector3(-5.0f, 0, 0);
                dir = 1;
                break;
            case 1: // Down
                targetPosition = initialPosition + new Vector3(0, -4.0f, 0);
                dir = 6;
                break;
        }

    }

    private void Update()
    {
        float prog = Mathf.Clamp01((Time.time - startTime) * 3 / Vector3.Distance(initialPosition, targetPosition));
        transform.position = Vector3.Lerp(initialPosition, targetPosition, prog);

        if (prog >= 1.0f)
        {
            initialPosition = transform.position;
            SetNextTargetPosition();
            startTime = Time.time;
        }
    }
}
