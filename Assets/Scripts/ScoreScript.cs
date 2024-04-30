using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

public class ScoreScript : MonoBehaviour
{
    private static ScoreScript _instance;

    public static ScoreScript Instance => _instance;
    private int score;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        score = 0;
        //Debug.Log("ScoreScript created");
    }

    public void UpdateScore()
    {
        score++;
    }
    
    public int GetScore()
    {
        return score;
    }

    public void WriteScore()
    {
        // display function that called this function
        System.IO.StreamWriter writer = new System.IO.StreamWriter(Application.persistentDataPath + "/score.txt", true);
        writer.WriteLine(GlobalVariables.Player.playerName + ":" + score);
        writer.Close();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        score = 0;
        //Debug.Log("ScoreScript created");
    }

    public List<string> GetScoreBoard()
    {
        // Read the file and only get the 10 highest scores in descending order stored as name:string:score:int
        List<string> scoreBoard = new List<string>();
        string[] lines = System.IO.File.ReadAllLines(Application.persistentDataPath + "/score.txt");
        List<Tuple<string, int>> scores = new List<Tuple<string, int>>();
        
        foreach (var line in lines)
        {
            string[] parts = line.Split(':');
            scores.Add(new Tuple<string, int>(parts[0], int.Parse(parts[1])));
        }
        
        scores.Sort((x, y) => y.Item2.CompareTo(x.Item2));
        for (int i=0; i<10 && i<scores.Count; i++)
        {
            scoreBoard.Add(scores[i].Item1 + ":" + scores[i].Item2);
        }
        return scoreBoard;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
