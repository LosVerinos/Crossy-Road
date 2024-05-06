using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manage_difficulty : MonoBehaviour
{
    public Button Easy_button;
    public Button Medium_button;
    public Button Hard_button;

    private Button selectedButton;

    void Start()
    {
        Easy_button.onClick.AddListener(Easy_click);
        Medium_button.onClick.AddListener(Medium_click);
        Hard_button.onClick.AddListener(Hard_click);
    }

    void Easy_click()
    {
        SetButtonState(Easy_button);
        GlobalVariables.difficulty = 1.0f;
    }

    void Medium_click()
    {
        SetButtonState(Medium_button);
        GlobalVariables.difficulty = 1.2f;
    }

    void Hard_click()
    {
        SetButtonState(Hard_button);
        GlobalVariables.difficulty = 1.5f;
    }

    void SetButtonState(Button clickedButton)
    {
        if (selectedButton != null)
        {
            selectedButton.interactable = true;

            ColorBlock colors = selectedButton.colors;
            colors.normalColor = Color.white;
            selectedButton.colors = colors;
        }

        selectedButton = clickedButton;

        clickedButton.interactable = false;

        ColorBlock clickedColors = clickedButton.colors;
        clickedColors.normalColor = new Color(0.6f, 0.6f, 0.6f);
        clickedButton.colors = clickedColors;
    }

}
