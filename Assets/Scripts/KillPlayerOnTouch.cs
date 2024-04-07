using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerOnTouch : MonoBehaviour
{

    public GameObject failPanel;

    private void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.CompareTag("Player"))
       {
           Destroy(other.gameObject);
           failPanel.SetActive(true);

           
       }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
