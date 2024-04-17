using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillPlayerOnTouch : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.CompareTag("Player"))
       {

           GlobalVariables.isPlayerKilled = true;
           GlobalVariables.run = false;

           Destroy(other.gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GlobalVariables.isPlayerKilled = true;
            GlobalVariables.run = false;

            Destroy(other.gameObject);

        }
    }
}
