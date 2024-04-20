using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buy_script : MonoBehaviour
{
    public Animator animator;

    public void buy_click()
    {
        if (PlayerPrefs.GetInt("Coins") > 100)
        {
            PlayAnimation("Buy_button");
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
            animator.Play(animationName);
        }
        else
        {
            Debug.LogWarning("L'Animator n'a pas été attribué au script.");
        }
    }
}