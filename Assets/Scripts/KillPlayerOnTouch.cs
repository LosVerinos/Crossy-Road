using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerOnTouch : MonoBehaviour
{
    public GameObject panel;

    private void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.CompareTag("Player"))
       {

            GlobalVariables.isPlayerKilled = true;

           Destroy(other.gameObject);

           Debug.Log("Première fonction");

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GlobalVariables.isPlayerKilled = true;

            Destroy(other.gameObject);

            Debug.Log("Deuxième fonction");

        }
    }
}
