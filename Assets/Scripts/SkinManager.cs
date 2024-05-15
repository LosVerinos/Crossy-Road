using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkinButtonManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform panel;
    public List<SkinData> skinDataList;

    private List<Button> buttons = new List<Button>();

    void Start()
    {
        CreateButtons();
        UpdateButtons();
    }

    void CreateButtons()
    {
        float buttonSpacing = 10f;
        float buttonSize = 180f;
        int maxButtonsPerColumn = 5;

        int buttonsInColumn = 0;
        int column = 0;
        int row = 0;

        float startXPosition = -280f;
        float startYPosition = -500f;

        foreach (SkinData skinData in skinDataList)
        {
            float xPosition = startXPosition + column * (buttonSize + buttonSpacing);
            float yPosition = startYPosition + row * (buttonSize + buttonSpacing);

            GameObject newButton = Instantiate(buttonPrefab, panel);
            RectTransform buttonTransform = newButton.GetComponent<RectTransform>();

            buttonTransform.localPosition = new Vector3(xPosition, yPosition, buttonTransform.localPosition.z);
            buttonTransform.sizeDelta = new Vector2(buttonSize, buttonSize);

            Button btnComponent = newButton.GetComponent<Button>();
            buttons.Add(btnComponent);

            Text txtComponent = newButton.GetComponentInChildren<Text>();
            if (txtComponent != null)
            {
                txtComponent.text = "";
            }

            Image imgComponent = newButton.GetComponentInChildren<Image>();
            if (imgComponent != null && skinData.sprite != null)
            {
                imgComponent.sprite = skinData.sprite;
            }

            btnComponent.onClick.AddListener(() => SelectSkin(skinData));

            buttonsInColumn++;
            if (buttonsInColumn >= maxButtonsPerColumn)
            {
                column++;
                row = 0;
                buttonsInColumn = 0;
            }
            else
            {
                row++;
            }
        }
    }

    public void UpdateButtons()
    {
        for (int i = 0; i < skinDataList.Count; i++)
        {
            SkinData skinData = skinDataList[i];
            Button btnComponent = buttons[i];

            if (!skinData.unlocked)
            {
                btnComponent.interactable = false;
                Image imgComponent = btnComponent.GetComponentInChildren<Image>();
                if (imgComponent != null)
                {
                    imgComponent.color = Color.gray;
                }
            }
            else
            {
                btnComponent.interactable = true;
                Image imgComponent = btnComponent.GetComponentInChildren<Image>();
                if (imgComponent != null)
                {
                    imgComponent.color = Color.white;
                }
            }
        }
    }

    void SelectSkin(SkinData skinData)
    {
        GlobalVariables.skin = skinData;
        GlobalVariables.theme = skinData.theme;

        foreach (SkinData skin in skinDataList)
        {
            skin.selected = false;
        }
        skinData.selected = true;

    }
}
