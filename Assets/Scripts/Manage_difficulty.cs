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

    private void Start()
    {
        Easy_button.onClick.AddListener(Easy_click);
        Medium_button.onClick.AddListener(Medium_click);
        Hard_button.onClick.AddListener(Hard_click);
    }

    public void Easy_click()
    {
        SetButtonState(Easy_button);
        GlobalVariables.difficulty = 1.0f;
    }

    public void Medium_click()
    {
        SetButtonState(Medium_button);
        GlobalVariables.difficulty = 1.2f;
    }

    public void Hard_click()
    {
        SetButtonState(Hard_button);
        GlobalVariables.difficulty = 1.5f;
    }

    private void SetButtonState(Button clickedButton)
    {
        if (selectedButton != null)
        {
            selectedButton.interactable = true;

            var colors = selectedButton.colors;
            colors.normalColor = Color.white;
            selectedButton.colors = colors;
        }

        selectedButton = clickedButton;

        clickedButton.interactable = false;

        var clickedColors = clickedButton.colors;
        clickedColors.normalColor = new Color(0.6f, 0.6f, 0.6f);
        clickedButton.colors = clickedColors;
    }
}