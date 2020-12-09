using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rocket;
    AudioSource rocketRumble;

    private float rotationSpeed;
    [SerializeField] float rcsThrust = 1;
    [SerializeField] float mainThrust = 1;

    // Start is called before the first frame update
    void Start()
    {
        rocket = GetComponent<Rigidbody>();
        rocketRumble = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RocketMovement();
        RocketRumble();
    }

    void RocketRumble()
    {
        if (Input.GetKey(KeyCode.Space) == true)
        {
            rocket.AddRelativeForce(Vector3.up * mainThrust);
        }

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            rocketRumble.Play(0);
        }

        if (Input.GetKeyUp(KeyCode.Space) == true)
        {
            rocketRumble.Stop();
        }

    }

    private void RocketMovement()
    {

        if (Input.GetKey(KeyCode.A) == true)
        {
            rocket.constraints = RigidbodyConstraints.FreezeRotationZ; //stops from rotating endlessly

            rotationSpeed = rcsThrust * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationSpeed);

            rocket.constraints = RigidbodyConstraints.None; //to unfreeze Z axis
        }
        else if (Input.GetKey(KeyCode.D) == true)
        {
            rocket.constraints = RigidbodyConstraints.FreezeRotationZ; //stops from rotating endlessly

            rotationSpeed = rcsThrust * Time.deltaTime;
            transform.Rotate(-Vector3.forward * rotationSpeed);

            rocket.constraints = RigidbodyConstraints.None; //to unfreeze Z axis
        }

        rocket.constraints = RigidbodyConstraints.FreezePositionZ;
        rocket.constraints = RigidbodyConstraints.FreezeRotationX;
        rocket.constraints = RigidbodyConstraints.FreezeRotationY;

    }
}
