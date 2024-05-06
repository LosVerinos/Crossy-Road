using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkinButtonManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform panel;
    public List<SkinData> skinDataList;
    public float buttonSpacing = 1f;
    public float buttonSize = 300f;
    public int maxButtonsPerColumn = 5;

    void Start()
    {
        float currentXPosition = 0f;
        float currentYPosition = 500f;

        int buttonsInColumn = 0;

        foreach (SkinData skinData in skinDataList)
        {
            GameObject newButton = Instantiate(buttonPrefab, panel);
            RectTransform buttonTransform = newButton.GetComponent<RectTransform>();
            buttonTransform.localPosition = new Vector3(currentXPosition, currentYPosition, buttonTransform.localPosition.z);
            buttonTransform.sizeDelta = new Vector2(buttonSize, buttonSize);

            if (!skinData.unlocked)
            {
                Button btnComponent2 = newButton.GetComponent<Button>();
                if (btnComponent2 != null)
                {
                    btnComponent2.interactable = false;
                }

                Image imgComponent2 = newButton.GetComponentInChildren<Image>();
                if (imgComponent2 != null)
                {
                    imgComponent2.color = Color.gray;
                }
            }

            currentYPosition -= buttonSize + buttonSpacing;
            buttonsInColumn++;

            if (buttonsInColumn >= maxButtonsPerColumn)
            {
                currentXPosition += buttonSize + buttonSpacing;
                currentYPosition = 500;
                buttonsInColumn = 0;
            }

            Text txtComponent = newButton.GetComponentInChildren<Text>();
            if (txtComponent != null)
            {
                txtComponent.text = skinData.theme;
            }

            Image imgComponent = newButton.GetComponentInChildren<Image>();
            if (imgComponent != null && skinData.sprite != null)
            {
                imgComponent.sprite = skinData.sprite;
            }

            Button btnComponent = newButton.GetComponent<Button>();
            if (btnComponent != null)
            {
                btnComponent.onClick.AddListener(() => SelectSkin(skinData));
            }
        }
    }

    void SelectSkin(SkinData skinData)
    {
        GlobalVariables.skin = skinData;
        GlobalVariables.theme = skinData.theme;

        foreach(SkinData skin in skinDataList)
        {
            skin.selected = false;
        }
        skinData.selected = true;
    }
}
