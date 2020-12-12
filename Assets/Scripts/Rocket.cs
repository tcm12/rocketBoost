using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rocket;

    [SerializeField] AudioClip rocketRumble;
    [SerializeField] AudioClip rcsTrhuster;
    AudioSource audioSource;

    [SerializeField] float rcsThrust = 1;
    [SerializeField] float mainThrust = 1;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    private bool dead = false;

    void Start()
    {
        rocket = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (dead)
        {
            //implement bombastique death
        }
        else
        {
            Freeze();
            RocketMovement();
            RocketRumble();
        }
    }

    void Freeze()
    {
        rocket.constraints = RigidbodyConstraints.FreezePositionZ;
        rocket.constraints = RigidbodyConstraints.FreezeRotationX;
        rocket.constraints = RigidbodyConstraints.FreezeRotationY;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (dead)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;
            case "Finish":
                if (!dead)
                {
                    Invoke("loadNextScene", 2f);
                }
                break;
            default:
                dead = true;
                audioSource.Stop();
                mainEngineParticles.Stop();
                leftThrusterParticles.Stop();
                rightThrusterParticles.Stop();
                Invoke("loadMainMenu", 2f);
                break;
        }
    }

    void loadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    void loadMainMenu()
    {
        SceneManager.LoadScene(0);
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
                   rightThrusterParticles.Play();
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
            rightThrusterParticles.Stop();
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
            rocket.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A) == true)
        {
            rocket.constraints = RigidbodyConstraints.FreezeRotationZ;
            Freeze();
            transform.Rotate(Vector3.forward * rcsThrust * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) == true)
        {
            rocket.constraints = RigidbodyConstraints.FreezeRotationZ;
            Freeze();
            transform.Rotate(-Vector3.forward * rcsThrust * Time.deltaTime);
        }
    }
}
