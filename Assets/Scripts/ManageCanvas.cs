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
    public GameObject scoreText;
    public Text timeText;
    public Text pseudo;

    public Animator animator;

    public Text coinsText;

    private bool visible = false;

    private float time = 0f;

    public void Update()
    {
        if (GlobalVariables.isPlayerKilled && startPanel != null) startPanel.SetActive(false);

        if (GlobalVariables.isPlayerKilled && visible == false && failPanel != null)
        {
            failPanel.SetActive(true);
            if (scoreText != null)
            {
                scoreText.transform.position = new Vector3(550f, 1800f, 0f);
                scoreText.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            visible = true;
        }

        if (coinsText != null)
        {
            coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
        }

        if (GlobalVariables.run && timeText != null)
        {
            time += Time.deltaTime;

            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);

            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (timeText.text != timeString)
            {
                timeText.text = timeString;
            }
        }
    }

    private void Start()
    {
        if(GlobalVariables.restart)
        {
            if (startPanel != null)
            {

                animator.Play("Start");
                //startPanel.SetActive(false);
                GlobalVariables.run = true;
                GlobalVariables.restart = false;
                

            }
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
        var currentScene = SceneManager.GetActiveScene();

        GlobalVariables.restart = true;

        SceneManager.LoadScene(currentScene.name);
    }


    public void menu()
    {
        Time.timeScale = 1;
        GlobalVariables.isPlayerKilled = false;
        GlobalVariables.run = false;
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        
    }
    public void Start_click()
    {
        GlobalVariables.run = true;
        GlobalVariables.Player.playerName = pseudo.text;
    }
    
    public void OnEnterLeaderBoard()
    {
        var scoreBoard = ScoreScript.Instance.GetScoreBoard("easy");
        var text = leaderBoardPanel.GetComponentInChildren<Text>();
        text.text = "";
        // sort the score board
        scoreBoard.Sort((x, y) => int.Parse(y.Split(':')[1]).CompareTo(int.Parse(x.Split(':')[1])));
        var i = 1;
        scoreBoard.ForEach(score =>
        {
            if (i > 10) return;
            text.text += i++ + ". " + score + "\n";
        });
    }
}
