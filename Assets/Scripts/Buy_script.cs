using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuyScript : MonoBehaviour
{
    public Animator animator;
    public Text coinsText;
    public List<SkinData> skinDataList;

    private bool available = true;
    private const int skinCost = 100;

    public void BuyClick()
    {
        int coins = PlayerPrefs.GetInt("Coins");

        if (coins >= skinCost && available)
        {
            PlayAnimation("Buy_button");

            coins -= skinCost;
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.Save();

            UpdateCoinsText(coins);
            UnlockRandomSkin();
        }
        else
        {
            PlayAnimation("Buy_failed");
        }
    }

    private void PlayAnimation(string animationName)
    {
        if (animator != null)
        {
            animator.StopPlayback();
            animator.Play(animationName);
        }
        else
        {
            Debug.LogWarning("Animator has not been assigned to the script.");
        }
    }

    private void UpdateCoinsText(int coins)
    {
        coinsText.text = coins.ToString();
    }

    private void UnlockRandomSkin()
    {
        List<SkinData> lockedSkins = skinDataList.Where(skin => !skin.unlocked).ToList();

        if (lockedSkins.Count > 0)
        {
            int randomIndex = Random.Range(0, lockedSkins.Count);
            SkinData randomSkin = lockedSkins[randomIndex];
            randomSkin.unlocked = true;

            Debug.Log($"Skin '{randomSkin.theme}' unlocked!");
        }
        else
        {
            Debug.Log("All skins are already unlocked!");
            available = false;
        }
    }
}
