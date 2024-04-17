using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageCanvas : MonoBehaviour
{

    public GameObject pausePanel;
    public GameObject pauseButtonPanel;
    public GameObject coinsPanel;
    public GameObject scorePanel;
    public GameObject failPanel;
    public GameObject leaderBoardPanel;
    public GameObject startPanel;

    private bool visible = false;

    public void Update()
    {
        if (GlobalVariables.isPlayerKilled && startPanel!=null)
        {
            startPanel.SetActive(false);
        }

        if (GlobalVariables.isPlayerKilled && visible == false && failPanel!=null)
        {
            Debug.Log("isplayed is currently setted on true");
            failPanel.SetActive(true);
            visible = true;
        }
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

    public void restart()
    {
        Time.timeScale = 1;
        GlobalVariables.isPlayerKilled = false;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        
    }
    public void Start_click()
    {
        GlobalVariables.run = true;
    }
    
    public void OnEnterLeaderBoard()
    {
        var scoreBoard = ScoreScript.Instance.GetScoreBoard();
        Text text = leaderBoardPanel.GetComponentInChildren<Text>();
        text.text = "";
        // sort the score board
        scoreBoard.Sort((x, y) => int.Parse(y.Split(':')[1]).CompareTo(int.Parse(x.Split(':')[1])));
        int i = 1;
        scoreBoard.ForEach(score =>
        {
            if (i > 10)
            {
                return;
            }
            text.text += i++ + ". " + score + "\n";
        });
    }
}
