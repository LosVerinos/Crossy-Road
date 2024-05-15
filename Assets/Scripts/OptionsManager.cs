using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public AudioSource audioSrc;
    public Slider sld;
    public Text txtVolume;

    // Start is called before the first frame update
    private void Start()
    {
        initSliderChange();
        SliderChange();
    }

    // Update is called once per frame
    private void Update()
    {
    }


    public void SliderChange()
    {
        audioSrc.volume = sld.value;
        txtVolume.text = "Volume : " + (sld.value * 100).ToString("00") + "%";

        PlayerPrefs.SetFloat("volume", sld.value);
        PlayerPrefs.Save();
    }

    private void initSliderChange()
    {
        sld.value = PlayerPrefs.GetFloat("volume");
        SliderChange();
    }
}