using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rocket;

    // Start is called before the first frame update
    void Start()
    {
        rocket = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space) == true)
        {
            rocket.AddRelativeForce(Vector3.up);
        }
        if (Input.GetKey(KeyCode.A) == true)
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D) == true)
             {
            transform.Rotate(-Vector3.forward);
        }
    }
}
