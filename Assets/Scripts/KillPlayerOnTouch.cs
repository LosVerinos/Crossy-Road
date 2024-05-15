using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerOnTouch : MonoBehaviour
{
    private void HandlePlayerCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            var playerScript = other.GetComponent<PlayerScript>();
            if (playerScript != null)
            {
                playerScript.KillPlayer();
            }
            else
            {
                Debug.LogWarning("Player object does not have a PlayerScript component attached.");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandlePlayerCollision(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandlePlayerCollision(other.gameObject);
    }
}
