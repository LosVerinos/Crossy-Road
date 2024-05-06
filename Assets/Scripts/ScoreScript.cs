using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private static ScoreScript _instance;
    public static ScoreScript Instance => _instance;
    private int score;
    public bool isCounting = false;
    private float currentDifficulty = 1.0f;

    public Button Easy_button;
    public Button Medium_button;
    public Button Hard_button;

    private Button selectedButton;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        score = 0;
    }

    public void UpdateScore()
    {
        score += Mathf.RoundToInt(currentDifficulty);
        isCounting = true;
    }

    public int GetScore()
    {
        return score;
    }

    public void WriteScore(string difficulty)
    {
        System.IO.StreamWriter writer = new System.IO.StreamWriter(Application.persistentDataPath + $"/score_{difficulty}.txt", true);
        writer.WriteLine(GlobalVariables.Player.playerName + ":" + score);
        writer.Close();
    }

    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        score = 0;

        
        Easy_button.onClick.AddListener(easy_click);
        Medium_button.onClick.AddListener(medium_click);
        Hard_button.onClick.AddListener(hard_click);
        
    }

    public List<string> GetScoreBoard(string difficulty)
    {
        List<string> scoreBoard = new List<string>();
        string[] lines = System.IO.File.ReadAllLines(Application.persistentDataPath + $"/score_{difficulty}.txt");
        List<Tuple<string, int>> scores = new List<Tuple<string, int>>();

        foreach (var line in lines)
        {
            string[] parts = line.Split(':');
            scores.Add(new Tuple<string, int>(parts[0], int.Parse(parts[1])));
        }

        scores.Sort((x, y) => y.Item2.CompareTo(x.Item2));
        for (int i = 0; i < 10 && i < scores.Count; i++)
        {
            scoreBoard.Add(scores[i].Item1 + ":" + scores[i].Item2);
        }
        return scoreBoard;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void SetDifficulty(float difficulty)
    {
        currentDifficulty = difficulty;
    }

    public void ReloadScoreBoard(string difficulty)
    {

        List<string> scoreBoard = GetScoreBoard(difficulty);

        foreach (string scoreEntry in scoreBoard)
        {
            Debug.Log(scoreEntry);
        }
    }

    public void easy_click()
    {
        SetButtonState(Easy_button);
        ReloadScoreBoard("easy");

    }
    public void medium_click()
    {
        SetButtonState(Medium_button);
        ReloadScoreBoard("medium");

    }
    public void hard_click()
    {
        SetButtonState(Hard_button);
        ReloadScoreBoard("hard");

    }

    void SetButtonState(Button clickedButton)
    {
        if (selectedButton != null)
        {
            selectedButton.interactable = true;

            ColorBlock colors = selectedButton.colors;
            colors.normalColor = Color.white;
            selectedButton.colors = colors;
        }

        selectedButton = clickedButton;

        clickedButton.interactable = false;

        ColorBlock clickedColors = clickedButton.colors;
        clickedColors.normalColor = new Color(0.6f, 0.6f, 0.6f);
        clickedButton.colors = clickedColors;
    }

}
