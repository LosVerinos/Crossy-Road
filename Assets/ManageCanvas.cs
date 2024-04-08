using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageCanvas : MonoBehaviour
{
    //working on the new things here
    public GameObject startMenuPanel;
    public GameObject shopPanel;
    public GameObject identityPanel;
    public GameObject pausePanel;
    public GameObject pauseButtonPanel;
    public GameObject failPanel;
    public GameObject leaderBoardPanel;
    public GameObject paramPanel;
    public GameObject paramButtonPanel;
    public GameObject coinsPanel;
    public GameObject scorePanel;


    public Animator animator;

    public void Start()
    {
        Debug.Log("scrip started well");
        
    }

    public void Update()
    {
        
    }


    public void pause_onClick()
    {
        scorePanel.SetActive(false);
        coinsPanel.SetActive(false);
        pauseButtonPanel.SetActive(false);

        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void DePause_onClick()
    {
        pausePanel.SetActive(false);

        scorePanel.SetActive(true);
        coinsPanel.SetActive(true);
        pauseButtonPanel.SetActive(true);

        Time.timeScale = 1;
    }

    public void StartButton_click()
    {
        paramButtonPanel.SetActive(false);
        startMenuPanel.SetActive(false);
    }

    public void IdentityButton_click()
    {
        startMenuPanel.SetActive(false);
        paramButtonPanel.SetActive(false);
        coinsPanel.SetActive(false);

        identityPanel.SetActive(true);
    }

    public void ShopButton_click()
    {
        startMenuPanel.SetActive(false);
        paramButtonPanel.SetActive(false);

        shopPanel.SetActive(true);
    }

    public void ClassementButton_click()
    {
        startMenuPanel.SetActive(false);
        paramButtonPanel.SetActive(false);
        coinsPanel.SetActive(false);

        leaderBoardPanel.SetActive(true);
    }

    public void ParamButton_click()
    {
        startMenuPanel.SetActive(false);
        paramButtonPanel.SetActive(false);
        coinsPanel.SetActive(false);

        paramPanel.SetActive(true);
    }

    public void PauseButton_click()
    {
        pausePanel.SetActive(true);
    }

    public void RightButton_click()
    {
        leaderBoardPanel.SetActive(false);

        startMenuPanel.SetActive(true);
        coinsPanel.SetActive(true);
        scorePanel.SetActive(true);
    }

    public void UpperButton_click()
    {
        //identityPanel.SetActive(false);

        startMenuPanel.SetActive(true);
        coinsPanel.SetActive(true);
        scorePanel.SetActive(true);
    }

    public void LeftButton_click()
    {
        shopPanel.SetActive(false);

        scorePanel.SetActive(true);
        startMenuPanel.SetActive(true);
    }

    public void MenuFromParamButton_click()
    {
        paramPanel.SetActive(false);

        startMenuPanel.SetActive(true);
        coinsPanel.SetActive(true);
        paramButtonPanel.SetActive(true);


    }

    public void PlayAnimation(string animationName)
    {
        if (animator != null)
        {
            animator.Play(animationName);
        }
        else
        {
            Debug.LogWarning("L'Animator n'est pas attribué au script.");
        }
    }






    public void QuiteGame()
    {
        Application.Quit();
    }


}
