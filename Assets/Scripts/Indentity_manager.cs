using UnityEngine;
using UnityEngine.UI;

public class Identity_manager : MonoBehaviour
{
    public Button natural_Button;
    public Button sw_Button;
    public Button lor_Button;
    public Button hp_Button;

    public GameObject natural_image;
    public GameObject sw_image;
    public GameObject lor_image;
    public GameObject hp_image;

    public Image natural;
    public Image sw;
    public Image lor;
    public Image hp;

    void Start()
    {
        UpdateThemeUI("Natural", natural_Button, natural_image, natural);
        UpdateThemeUI("StarWars", sw_Button, sw_image, sw);
        UpdateThemeUI("LordOfRings", lor_Button, lor_image, lor);
        UpdateThemeUI("HarryPotters", hp_Button, hp_image, hp);
}

    void UpdateThemeUI(string themeName, Button button, GameObject image, Image image_button)
    {
        bool themeAcquired = GetThemeAcquired(themeName);
        button.interactable = themeAcquired;
        if (!themeAcquired)
        {
            UnityEngine.UI.Image img = image.GetComponent<UnityEngine.UI.Image>();
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0.5f);
            image_button.color = new Color(image_button.color.r, image_button.color.g, image_button.color.b, 0.5f);
        }
    }

    bool GetThemeAcquired(string themeName)
    {
        bool acquired = PlayerPrefs.GetInt(themeName) == 1;
        return acquired;
    }
}
