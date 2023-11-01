using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    private Vector3 currentInput = Vector3.zero;
    private Vector3 lastInput = Vector3.zero;
    private bool moving = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) lastInput = Vector3.up;
        if (Input.GetKey(KeyCode.A)) lastInput = Vector3.left;
        if (Input.GetKey(KeyCode.S)) lastInput = Vector3.down;
        if (Input.GetKey(KeyCode.D)) lastInput = Vector3.right;

        if (!moving)
        {

            Vector3 targetPosition = transform.position + currentInput;

            if (IsWalkable(targetPosition))
            {
                StartCoroutine(LerpToPosition(targetPosition));
            }
        }
    }
    
    IEnumerator LerpToPosition(Vector3 target)
    {
        moving = true;
        float journeyLength = Vector3.Distance(transform.position, target);
        float journeyTime = journeyLength / moveSpeed;
        float startTime = Time.time;

        while (Time.time < startTime + journeyTime)
        {
            float journeyFraction = (Time.time - startTime) / journeyTime;
            transform.position = Vector3.Lerp(transform.position, target, journeyFraction);
            yield return null;
        }

        transform.position = target;
        moving = false;
    }

    bool IsWalkable(Vector3 position)
    {
        return true;
    }
}
