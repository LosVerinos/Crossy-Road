using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Dropdown DResolution;
    public AudioSource audioSrc;
    public Slider sld;
    public Text txtVolume;




    // Start is called before the first frame update
    private void Start()
    {
        InitializeResolutionsDropdown();
        SliderChange();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
