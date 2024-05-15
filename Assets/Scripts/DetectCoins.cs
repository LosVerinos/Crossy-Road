using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectCoins : MonoBehaviour
{
    private bool isColliding = false;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !isColliding)
        {
            isColliding = true;

            var coin = PlayerPrefs.GetInt("Coins");

            coin++;

            PlayerPrefs.SetInt("Coins", coin);
            PlayerPrefs.Save();


            //GlobalVariables.coins++;
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        isColliding = false;

        if (GlobalVariables.reload)
            if (gameObject != null)
                Destroy(gameObject);
    }
}