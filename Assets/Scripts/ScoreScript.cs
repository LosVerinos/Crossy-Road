using System;
using System.Collections.Generic;
using System.IO;
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

    [SerializeField] private Text scoreText;
    [SerializeField] private Button easyButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button hardButton;
    [SerializeField] private Text timerText;
    [SerializeField] private Text rankText;

    private Button selectedButton;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        score = 0;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CheckFiles();

        easyButton.onClick.AddListener(() => ChangeDifficulty("easy", 1.0f));
        mediumButton.onClick.AddListener(() => ChangeDifficulty("medium", 1.2f));
        hardButton.onClick.AddListener(() => ChangeDifficulty("hard", 1.5f));
    }

    public void UpdateScore()
    {
        score += Mathf.RoundToInt(currentDifficulty);
        isCounting = true;
        scoreText.text = "Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }

    public void WriteScore(string difficulty)
    {
        string path = Path.Combine(Application.persistentDataPath, $"score_{difficulty}.txt");
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine($"{GlobalVariables.Player.playerName}:{score}");
        }
    }

    public List<string> GetScoreBoard(string difficulty)
    {
        string path = Path.Combine(Application.persistentDataPath, $"score_{difficulty}.txt");
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
            return new List<string> { "1:0" };
        }

        var lines = File.ReadAllLines(path);
        var scores = lines.Select(line =>
        {
            var parts = line.Split(':');
            return new Tuple<string, int>(parts[0], int.Parse(parts[1]));
        }).ToList();

        scores.Sort((x, y) => y.Item2.CompareTo(x.Item2));
        return scores.Take(10).Select(score => $"{score.Item1}:{score.Item2}").ToList();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }

    public void SetDifficulty(float difficulty)
    {
        currentDifficulty = difficulty;
    }

    public void ReloadScoreBoard(string difficulty)
    {
        var scoreBoard = GetScoreBoard(difficulty);
        rankText.text = "";
        for (int i = 0; i < scoreBoard.Count; i++)
        {
            rankText.text += $"{i + 1} - {scoreBoard[i]}\n";
        }
    }

    private void ChangeDifficulty(string difficulty, float difficultyValue)
    {
        SetButtonState(difficulty == "easy" ? easyButton : difficulty == "medium" ? mediumButton : hardButton);
        SetDifficulty(difficultyValue);
        ReloadScoreBoard(difficulty);
    }

    private void SetButtonState(Button clickedButton)
    {
        if (selectedButton != null)
        {
            selectedButton.interactable = true;
            var colors = selectedButton.colors;
            colors.normalColor = Color.white;
            selectedButton.colors = colors;
        }

        selectedButton = clickedButton;
        clickedButton.interactable = false;

        var clickedColors = clickedButton.colors;
        clickedColors.normalColor = new Color(0.6f, 0.6f, 0.6f);
        clickedButton.colors = clickedColors;
    }

    private void CheckFiles()
    {
        string[] difficulties = { "easy", "medium", "hard" };
        foreach (var difficulty in difficulties)
        {
            string path = Path.Combine(Application.persistentDataPath, $"score_{difficulty}.txt");
            if (!File.Exists(path))
            {
                using (var writer = new StreamWriter(path, true))
                {
                    writer.WriteLine("1:0");
                }
            }
        }
    }
}
