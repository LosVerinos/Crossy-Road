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

    public void QuiteGame()
    {
        Application.Quit();
    }


}
