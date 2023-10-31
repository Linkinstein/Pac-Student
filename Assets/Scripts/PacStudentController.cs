using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private string lastInput = "";

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) lastInput = "";
        if (Input.GetKey(KeyCode.A)) lastInput = "";
        if (Input.GetKey(KeyCode.S)) lastInput = "";
        if (Input.GetKey(KeyCode.D)) lastInput = "";

    }
}
