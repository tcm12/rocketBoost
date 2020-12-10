using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rocket;

    [SerializeField] AudioClip rocketRumble;

    //Modify the audio file to be longer
    [SerializeField] AudioClip rcsTrhuster;
    AudioSource audioSource;

    private float rotationSpeed;
    [SerializeField] float rcsThrust = 1;
    [SerializeField] float mainThrust = 1;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightTrhusterParticles;

    // Start is called before the first frame update
    void Start()
    {
        rocket = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RocketMovement();
        RocketRumble();
    }

    void RocketRumble()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            audioSource.PlayOneShot(rocketRumble);
            mainEngineParticles.Play();
        }

        if (Input.GetKeyDown(KeyCode.A) == true)
        {
            audioSource.PlayOneShot(rcsTrhuster);
            leftThrusterParticles.Play();
        }
        else if (Input.GetKeyDown(KeyCode.D) == true)
             {
                   audioSource.PlayOneShot(rcsTrhuster);
                   rightTrhusterParticles.Play();
             }

        if (Input.GetKeyUp(KeyCode.Space) == true)
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
            if (Input.GetKey(KeyCode.A) == true || Input.GetKey(KeyCode.D) == true)
            {
                audioSource.PlayOneShot(rcsTrhuster);
            }
        }

        if (Input.GetKeyUp(KeyCode.A) == true)
        {
            audioSource.Stop();
            leftThrusterParticles.Stop();
            if (Input.GetKey(KeyCode.Space) == true)
            {
                audioSource.PlayOneShot(rocketRumble);
            }
            if (Input.GetKey(KeyCode.D) == true)
            {
                audioSource.PlayOneShot(rcsTrhuster);
            }
        }

        if (Input.GetKeyUp(KeyCode.D) == true)
        {
            audioSource.Stop();
            rightTrhusterParticles.Stop();
            if (Input.GetKey(KeyCode.Space) == true)
            {
                audioSource.PlayOneShot(rocketRumble);
            }
            if (Input.GetKey(KeyCode.A) == true)
            {
                audioSource.PlayOneShot(rcsTrhuster);
            }
        }
    }

    private void RocketMovement()
    {
        if (Input.GetKey(KeyCode.Space) == true)
        {
            rocket.AddRelativeForce(Vector3.up * mainThrust);
        }

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
