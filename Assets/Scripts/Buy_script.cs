using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buy_script : MonoBehaviour
{
    public Animator animator;
    public Text coinsText;
    public List<SkinData> skinDataList;

    public void buy_click()
    {

        if (PlayerPrefs.GetInt("Coins") >= 1)
        {
            PlayAnimation("Buy_button");

            int coins = PlayerPrefs.GetInt("Coins");

            coins = coins - 100;

            PlayerPrefs.SetInt("Coins", coins);

            PlayerPrefs.Save();

            coinsText.text = PlayerPrefs.GetInt("Coins").ToString();

            UnlockRandomSkin();

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

    public void UnlockRandomSkin()
    {
        List<SkinData> lockedSkins = new List<SkinData>();

        foreach (SkinData skinData in skinDataList)
        {
            if (!skinData.unlocked)
            {
                lockedSkins.Add(skinData);
            }
        }

        if (lockedSkins.Count > 0)
        {
            int randomIndex = Random.Range(0, lockedSkins.Count);
            SkinData randomSkin = lockedSkins[randomIndex];

            randomSkin.unlocked = true;

            Debug.Log("Skin '" + randomSkin.theme + "' unlocked!");
        }
        else
        {
            Debug.LogError("Tous les skins sont déjà déverrouillés!");
        }
    }
}
