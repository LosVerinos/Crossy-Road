using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    // Panels for different game states
    public GameObject startMenuPanel;
    public GameObject shopPanel;
    public GameObject runPanel;
    public GameObject identityPanel;
    public GameObject pausePanel;
    public GameObject failPanel;
    public GameObject leaderBoardPanel;
    public GameObject paramPanel;

    // Options panel and resolution dropdown
    public GameObject optionsPanel;
    public Dropdown resolutionDropdown;

    // Audio settings
    public AudioSource audioSource;
    public Slider volumeSlider;
    public Text volumeText;

    private bool isVisible = false;

    private void Start()
    {
        InitializeResolutionsDropdown();
        UpdateVolume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isVisible = !isVisible;
            optionsPanel.SetActive(isVisible);
        }
    }

    public void PauseGame()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetResolution()
    {
        var selectedResolution = Screen.resolutions[resolutionDropdown.value];
        var targetAspectRatio = 16f / 9f;
        var targetWidth = Mathf.RoundToInt(selectedResolution.height * targetAspectRatio);
        Screen.SetResolution(targetWidth, selectedResolution.height, true);
    }

    public void UpdateVolume()
    {
        audioSource.volume = volumeSlider.value;
        volumeText.text = $"Volume: {(volumeSlider.value * 100):00}%";
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void InitializeResolutionsDropdown()
    {
        resolutionDropdown.ClearOptions();

        var resolutionOptions = new List<string>();
        foreach (var resolution in Screen.resolutions)
        {
            var resolutionString = $"{resolution.width} x {resolution.height}";
            if (!resolutionOptions.Contains(resolutionString))
            {
                resolutionOptions.Add(resolutionString);
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);

        var currentResolution = Screen.currentResolution;
        var currentResolutionString = $"{currentResolution.width} x {currentResolution.height}";
        resolutionDropdown.value = resolutionOptions.IndexOf(currentResolutionString);

        resolutionDropdown.RefreshShownValue();
    }

    public void ShowMenu()
    {
        startMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
}
