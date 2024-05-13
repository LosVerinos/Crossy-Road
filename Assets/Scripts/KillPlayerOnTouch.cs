using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class KillPlayerOnTouch : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.CompareTag("Player"))
       {
           other.gameObject.GetComponent<PlayerScript>().KillPlayer();
       }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerScript>().KillPlayer();

        }
    }
}
