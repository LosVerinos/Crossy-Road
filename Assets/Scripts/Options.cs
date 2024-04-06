using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    //working on the new things here
    public GameObject startMenuPanel;
    public GameObject shopPanel;
    public GameObject runPanel;
    public GameObject identityPanel;
    public GameObject pausePanel;
    public GameObject failPanel;
    public GameObject leaderBoardPanel;
    public GameObject paramPanel;




    public GameObject Panel;
    public Dropdown DResolution;
    bool visible = false;

    public GameObject PausePanel;

    public GameObject OptionPanel;
    public GameObject StartPanel;

    public AudioSource audioSrc;
    public Slider sld;
    public Text txtVolume;

    private void Start()
    {
        InitializeResolutionsDropdown();
        SliderChange();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            visible = !visible;
            Panel.SetActive(visible);
            
        }
    }

    public void pause_onClick()
    {
        Panel.SetActive(false);
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void DePause_onClick()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }


    public void SetResolution()
    {
        Resolution selectedResolution = Screen.resolutions[DResolution.value];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, true);
    }

    public void SliderChange()
    {
        audioSrc.volume = sld.value;
        txtVolume.text = "Volume : " + (sld.value * 100).ToString("00") + "%";
    }

    public void QuiteGame()
    {
        Application.Quit();
    }

    private void InitializeResolutionsDropdown()
    {
        
        DResolution.ClearOptions();

        List<string> resolutionOptions = new List<string>();

        foreach (Resolution resolution in Screen.resolutions)
        {
            string resolutionString = $"{resolution.width} x {resolution.height}";
            if (!resolutionOptions.Contains(resolutionString))
            {
                resolutionOptions.Add(resolutionString);
            }
        }

        DResolution.AddOptions(resolutionOptions);

        Resolution currentResolution = Screen.currentResolution;
        string currentResolutionString = $"{currentResolution.width} x {currentResolution.height}";
        DResolution.value = resolutionOptions.IndexOf(currentResolutionString);

        DResolution.RefreshShownValue();
    }

    public void ShowMenu()
    {
        StartPanel.SetActive(false);
        OptionPanel.SetActive(true);
    }
}
