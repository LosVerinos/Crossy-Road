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

            int coin = PlayerPrefs.GetInt("Coins");

            coin++;

            PlayerPrefs.SetInt("Coins", coin);
            PlayerPrefs.Save();


            //GlobalVariables.coins++;
            GetComponent<AiScript>().AddReward(+0.5f);
            Destroy(this.gameObject);
        }
    }

    public void Update()
    {
        isColliding = false;
    }
}
