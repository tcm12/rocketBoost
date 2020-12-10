﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rocket;

    [SerializeField] AudioClip rocketRumble;

    [SerializeField] AudioClip rcsTrhuster;
    AudioSource audioSource;

    private float rotationSpeed;
    [SerializeField] float rcsThrust = 1;
    [SerializeField] float mainThrust = 1;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightTrhusterParticles;

    void Start()
    {
        rocket = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Freeze();
        RocketMovement();
        RocketRumble();
    }

    void Freeze()
    {
        rocket.constraints = RigidbodyConstraints.FreezePositionZ;
        rocket.constraints = RigidbodyConstraints.FreezeRotationX;
        rocket.constraints = RigidbodyConstraints.FreezeRotationY;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;
            case "Finish":
                //win
                break;
            default:
                //destroy rocket
                break;
        }
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
            rocket.constraints = RigidbodyConstraints.FreezeRotationZ;
            Freeze();
            rotationSpeed = rcsThrust * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D) == true)
        {
            rocket.constraints = RigidbodyConstraints.FreezeRotationZ;
            Freeze();
            rotationSpeed = rcsThrust * Time.deltaTime;
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
    }
}
