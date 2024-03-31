using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public GameObject Panel;
    public Dropdown DResolution;
    bool visible = false;

    public AudioSource audioSrc;
    public Slider sld;
    public Text txtVolume;

    private void Start() => SliderChange();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            visible = !visible;
            Panel.SetActive(visible);
        }
    }

    public void SetResolution()
    {
        switch (DResolution.value)
        {
            case 0:
                Screen.SetResolution(640, 360, true);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }

    public void SliderChange()
    {
        audioSrc.volume = sld.value;
        txtVolume.text = "Volume : " + (sld.value * 100).ToString("00") + "%";
    }
}
