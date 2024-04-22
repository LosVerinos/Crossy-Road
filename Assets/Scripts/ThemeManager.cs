using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    private const string themesKey = "Themes";

    private Dictionary<string, bool> themesAcquired = new Dictionary<string, bool>();

    void Start()
    {
        LoadThemes();
    }

    public void InitializeThemes()
    {
        themesAcquired.Add("StarWars", false);
        themesAcquired.Add("HarryPotters", false);
    }

    private void LoadThemes()
    {
        if (PlayerPrefs.HasKey(themesKey))
        {
            string themesJson = PlayerPrefs.GetString(themesKey);
            themesAcquired = JsonUtility.FromJson<Dictionary<string, bool>>(themesJson);
        }
        else
        {
            InitializeThemes();
            SaveThemes();
        }
    }

    public void SaveThemes()
    {
        string themesJson = JsonUtility.ToJson(themesAcquired);
        PlayerPrefs.SetString(themesKey, themesJson);
        PlayerPrefs.Save();
    }

    public bool GetThemeAcquired(string themeName)
    {
        if (themesAcquired.ContainsKey(themeName))
        {
            return themesAcquired[themeName];
        }
        else
        {
            Debug.LogWarning("Le thème '" + themeName + "' n'existe pas dans le dictionnaire de thèmes.");
            return false;
        }
    }

    public void SetThemeAcquired(string themeName, bool acquired)
    {
        if (themesAcquired.ContainsKey(themeName))
        {
            themesAcquired[themeName] = acquired;
            SaveThemes();
        }
        else
        {
            Debug.LogWarning("Le thème '" + themeName + "' n'existe pas dans le dictionnaire de thèmes.");
        }
    }
}
