using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider volumeSlider;
    public Text volumeText;

    private const string VolumePrefKey = "volume";

    // Start is called before the first frame update
    private void Start()
    {
        InitializeVolumeSettings();
    }

    public void OnVolumeSliderChange()
    {
        float volume = volumeSlider.value;
        audioSource.volume = volume;
        volumeText.text = $"Volume: {(volume * 100):00}%";

        PlayerPrefs.SetFloat(VolumePrefKey, volume);
        PlayerPrefs.Save();
    }

    private void InitializeVolumeSettings()
    {
        if (PlayerPrefs.HasKey(VolumePrefKey))
        {
            volumeSlider.value = PlayerPrefs.GetFloat(VolumePrefKey);
        }
        else
        {
            volumeSlider.value = 1f; // Default volume value
        }
        OnVolumeSliderChange();
    }
}
