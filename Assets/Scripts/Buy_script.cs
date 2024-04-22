using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buy_script : MonoBehaviour
{
    public Animator animator;
    public Text coinsText;


    public void buy_click()
    {

        if (PlayerPrefs.GetInt("Coins") >= 100)
        {
            PlayAnimation("Buy_button");

            int coins = PlayerPrefs.GetInt("Coins");

            coins = coins - 100;

            PlayerPrefs.SetInt("Coins", coins);

            PlayerPrefs.Save();

            coinsText.text = PlayerPrefs.GetInt("Coins").ToString();

            CheckAndSetFirstUnacquiredTheme();

        }
        else
        {
            PlayAnimation("Buy_failled");
        }
    }

    void PlayAnimation(string animationName)
    {
        if (animator != null)
        {
            animator.StopPlayback();

            animator.Play(animationName);
        }
        else
        {
            Debug.LogWarning("L'Animator n'a pas été attribué au script.");
        }
    }

    private void CheckAndSetFirstUnacquiredTheme()
    {
        if (PlayerPrefs.GetInt("StarWars") == 0)
        {
            PlayerPrefs.SetInt("StarWars", 1);
            PlayerPrefs.Save();
            return;
        }
        if (PlayerPrefs.GetInt("HarryPotters") == 0)
        {
            PlayerPrefs.SetInt("HarryPotters", 1);
            PlayerPrefs.Save();
            return;
        }
        if (PlayerPrefs.GetInt("LordOfRings") == 0)
        {
            PlayerPrefs.SetInt("LordOfRings", 1);
            PlayerPrefs.Save();
            return;
        }
    }
}
