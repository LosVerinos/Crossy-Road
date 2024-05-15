using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCoins : MonoBehaviour
{
    private bool isColliding = false;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !isColliding)
        {
            isColliding = true;

            IncrementCoinCount();

            Destroy(gameObject);
        }
    }

    private void IncrementCoinCount()
    {
        int coin = PlayerPrefs.GetInt("Coins", 0);
        coin++;
        PlayerPrefs.SetInt("Coins", coin);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        ResetCollisionFlag();

        if (GlobalVariables.reload && gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    private void ResetCollisionFlag()
    {
        isColliding = false;
    }
}
