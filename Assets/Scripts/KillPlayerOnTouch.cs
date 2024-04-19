using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerOnTouch : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.CompareTag("Player"))
       {
           other.gameObject.GetComponent<PlayerScript>().KillPlayer();
           
       }
      else if (other.gameObject.CompareTag("Agent")){
        Debug.Log("Killed Trigger");
        other.gameObject.GetComponent<AIAgentScript>().SetReward(-5f);
        other.gameObject.GetComponent<AIAgentScript>().EndEpisode();
      }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerScript>().KillPlayer();
        }
        else if (other.gameObject.CompareTag("Agent")){
          Debug.Log("Killed Trigger");
          other.gameObject.GetComponent<AIAgentScript>().SetReward(-5f);
          other.gameObject.GetComponent<AIAgentScript>().EndEpisode();
        }
    }
}
