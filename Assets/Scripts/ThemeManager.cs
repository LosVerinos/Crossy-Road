using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    private const string THEME_STARWARS = "StarWars";
    private const string THEME_HARRYPOTTERS = "HarryPotters";
    private const string THEME_NATURAL = "Natural";
    private const string THEME_LORDOFRINGS = "LordOfRings";

    private int themeStarWarsAcquired;
    private int themeHarryPottersAcquired;
    private int themeNaturalAcquired;
    private int themeLordOfRingsAcquired;

    void Start()
    {
     
    }

    public void InitializeThemes()
    {
        themeStarWarsAcquired = 0;
        themeHarryPottersAcquired = 0;
        themeNaturalAcquired = 1;
        themeLordOfRingsAcquired = 0;
    }

    private void LoadThemes()
    {
        themeStarWarsAcquired = PlayerPrefs.GetInt(THEME_STARWARS);
        themeHarryPottersAcquired = PlayerPrefs.GetInt(THEME_HARRYPOTTERS);
        themeNaturalAcquired = PlayerPrefs.GetInt(THEME_NATURAL);
        themeLordOfRingsAcquired = PlayerPrefs.GetInt(THEME_LORDOFRINGS);
    }

    public void SaveThemes()
    {
        PlayerPrefs.SetInt(THEME_STARWARS, themeStarWarsAcquired);
        PlayerPrefs.SetInt(THEME_HARRYPOTTERS, themeHarryPottersAcquired);
        PlayerPrefs.SetInt(THEME_NATURAL, themeNaturalAcquired);
        PlayerPrefs.SetInt(THEME_LORDOFRINGS, themeLordOfRingsAcquired);

        PlayerPrefs.Save();

        Debug.Log("Theme StarWars acquired: " + themeStarWarsAcquired);
        Debug.Log("Theme HarryPotters acquired: " + themeHarryPottersAcquired);
        Debug.Log("Theme Natural acquired: " + themeNaturalAcquired);
        Debug.Log("Theme LordOfRings acquired: " + themeLordOfRingsAcquired);
    }

    public int GetThemeAcquired(string themeName)
    {
        switch (themeName)
        {
            case THEME_STARWARS:
                return themeStarWarsAcquired;
            case THEME_HARRYPOTTERS:
                return themeHarryPottersAcquired;
            case THEME_NATURAL:
                return themeNaturalAcquired;
            case THEME_LORDOFRINGS:
                return themeLordOfRingsAcquired;
            default:
                Debug.LogWarning("Theme '" + themeName + "' is not recognized.");
                return 0;
        }
    }

    public void SetThemeAcquired(string themeName, int acquired)
    {
        switch (themeName)
        {
            case THEME_STARWARS:
                themeStarWarsAcquired = acquired;
                break;
            case THEME_HARRYPOTTERS:
                themeHarryPottersAcquired = acquired;
                break;
            case THEME_NATURAL:
                themeNaturalAcquired = acquired;
                break;
            case THEME_LORDOFRINGS:
                themeLordOfRingsAcquired = acquired;
                break;
            default:
                Debug.LogWarning("Theme '" + themeName + "' is not recognized.");
                break;
        }

        SaveThemes();
    }

    public void RemoveTheme(string themeName)
    {
        PlayerPrefs.DeleteKey(themeName);
        PlayerPrefs.Save();
    }
}
