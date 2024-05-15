using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageDifficulty : MonoBehaviour
{
    [Header("Buttons")]
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    private Button selectedButton;

    private void Start()
    {
        easyButton.onClick.AddListener(() => SetDifficulty(easyButton, 1.0f));
        mediumButton.onClick.AddListener(() => SetDifficulty(mediumButton, 1.2f));
        hardButton.onClick.AddListener(() => SetDifficulty(hardButton, 1.5f));
    }

    private void SetDifficulty(Button button, float difficulty)
    {
        if (selectedButton != null)
        {
            selectedButton.interactable = true;
            UpdateButtonColor(selectedButton, Color.white);
        }

        selectedButton = button;
        selectedButton.interactable = false;
        UpdateButtonColor(selectedButton, new Color(0.6f, 0.6f, 0.6f));

        GlobalVariables.difficulty = difficulty;
    }

    private void UpdateButtonColor(Button button, Color color)
    {
        var colors = button.colors;
        colors.normalColor = color;
        button.colors = colors;
    }
}
