using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkinButtonManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform panel;
    public List<SkinData> skinDataList;


    private void Start()
    {
        var buttonSpacing = 10f;
        var buttonSize = 180f;
        var maxButtonsPerColumn = 5;

        var totalButtons = skinDataList.Count;
        var buttonsInColumn = 0;
        var column = 0;
        var row = 0;

        var startXPosition = -280f;
        var startYPosition = -500f;

        foreach (var skinData in skinDataList)
        {
            var xPosition = startXPosition + column * (buttonSize + buttonSpacing);
            var yPosition = startYPosition + row * (buttonSize + buttonSpacing);

            var newButton = Instantiate(buttonPrefab, panel);
            var buttonTransform = newButton.GetComponent<RectTransform>();

            buttonTransform.localPosition = new Vector3(xPosition, yPosition, buttonTransform.localPosition.z);
            buttonTransform.sizeDelta = new Vector2(buttonSize, buttonSize);

            if (!skinData.unlocked)
            {
                var btnComponent = newButton.GetComponent<Button>();
                if (btnComponent != null) btnComponent.interactable = false;

                var imgComponent_ = newButton.GetComponentInChildren<Image>();
                if (imgComponent_ != null) imgComponent_.color = Color.gray;
            }

            var txtComponent = newButton.GetComponentInChildren<Text>();
            if (txtComponent != null) txtComponent.text = "";

            var imgComponent = newButton.GetComponentInChildren<Image>();
            if (imgComponent != null && skinData.sprite != null) imgComponent.sprite = skinData.sprite;

            var btnComponentListener = newButton.GetComponent<Button>();
            if (btnComponentListener != null) btnComponentListener.onClick.AddListener(() => SelectSkin(skinData));

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


    private void SelectSkin(SkinData skinData)
    {
        GlobalVariables.skin = skinData;
        GlobalVariables.theme = skinData.theme;

        foreach (var skin in skinDataList) skin.selected = false;
        skinData.selected = true;
    }
}