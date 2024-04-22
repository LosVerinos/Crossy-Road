using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public GameManager Instance => _instance;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ShowMainMenu()
    {
        // Not implemented
    }

    public void HideMainMenu()
    {
        // TODO: Implement
    }

    public void StartGame()
    {
        // TODO: Implement
    }

    public void GameOver()
    {
        
    }
    
    
}
