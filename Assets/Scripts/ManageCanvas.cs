using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageCanvas : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pausePanel;
    public GameObject pauseButtonPanel;
    public GameObject coinsPanel;
    public GameObject scorePanel;
    public GameObject failPanel;
    public GameObject leaderBoardPanel;
    public GameObject startPanel;
    public GameObject scoreText;

    [Header("UI Texts")]
    public Text timeText;
    public Text coinsText;

    [Header("Animator")]
    public Animator animator;

    private bool isFailPanelVisible = false;
    private float elapsedTime = 0f;

    private void Start()
    {
        if (GlobalVariables.restart && startPanel != null)
        {
            animator.Play("Start");
            GlobalVariables.run = true;
            GlobalVariables.restart = false;
        }
    }

    private void Update()
    {
        HandlePlayerDeath();
        UpdateCoinsText();
        UpdateTimeText();
    }

    private void HandlePlayerDeath()
    {
        if (GlobalVariables.isPlayerKilled)
        {
            if (startPanel != null) startPanel.SetActive(false);

            if (!isFailPanelVisible && failPanel != null)
            {
                failPanel.SetActive(true);
                if (scoreText != null)
                {
                    scoreText.transform.position = new Vector3(550f, 1800f, 0f);
                    scoreText.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
                isFailPanelVisible = true;
            }
        }
    }

    private void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
        }
    }

    private void UpdateTimeText()
    {
        if (GlobalVariables.run && timeText != null)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            string timeString = $"{minutes:00}:{seconds:00}";

            if (timeText.text != timeString)
            {
                timeText.text = timeString;
            }
        }
    }

    public void PauseGame()
    {
        SetGameUI(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        SetGameUI(true);
        Time.timeScale = 1;
    }

    private void SetGameUI(bool isActive)
    {
        scorePanel.SetActive(isActive);
        coinsPanel.SetActive(isActive);
        pauseButtonPanel.SetActive(isActive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        GlobalVariables.isPlayerKilled = false;
        GlobalVariables.restart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        GlobalVariables.isPlayerKilled = false;
        GlobalVariables.run = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        GlobalVariables.run = true;
    }

    public void ShowLeaderBoard()
    {
        var scoreBoard = ScoreScript.Instance.GetScoreBoard("easy");
        var text = leaderBoardPanel.GetComponentInChildren<Text>();
        text.text = "";

        scoreBoard.Sort((x, y) => int.Parse(y.Split(':')[1]).CompareTo(int.Parse(x.Split(':')[1])));
        for (int i = 0; i < Mathf.Min(scoreBoard.Count, 10); i++)
        {
            text.text += $"{i + 1}. {scoreBoard[i]}\n";
        }
    }
}
