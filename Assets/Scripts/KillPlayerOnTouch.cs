using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerOnTouch : MonoBehaviour
{
    public Canvas canvas;
    private Animator Animator;
    public string animatonName = "Fail";

    private void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.CompareTag("Player"))
       {
           Destroy(other.gameObject);

           Animator = canvas.GetComponent<Animator>();

           Animator.Play(animatonName);

           Debug.Log("Première fonction");

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);

            Animator = GetComponent<Animator>();

            Animator.Play(animatonName);

            Debug.Log("Deuxième fonction");

        }
    }
}
