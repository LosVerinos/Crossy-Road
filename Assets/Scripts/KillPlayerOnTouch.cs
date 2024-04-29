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
           // other.gameObject.GetComponent<PlayerScript>().KillPlayer();
           other.gameObject.GetComponent<AiScript>().AddReward(-0.8f);
           other.gameObject.GetComponent<AiScript>().EndEpisode();

       }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // other.gameObject.GetComponent<PlayerScript>().KillPlayer();
            other.gameObject.GetComponent<AiScript>().AddReward(-0.8f);
            other.gameObject.GetComponent<AiScript>().EndEpisode();

        }
    }
}
