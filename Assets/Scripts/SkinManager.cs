using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkinButtonManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform panel;
    public List<SkinData> skinDataList;
    

    void Start()
    {
        float buttonSpacing = 10f;
        float buttonSize = 180f;
        int maxButtonsPerColumn = 5;

        int totalButtons = skinDataList.Count;
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

            if (!skinData.unlocked)
            {
                Button btnComponent = newButton.GetComponent<Button>();
                if (btnComponent != null)
                {
                    btnComponent.interactable = false;
                }

                Image imgComponent_ = newButton.GetComponentInChildren<Image>();
                if (imgComponent_ != null)
                {
                    imgComponent_.color = Color.gray;
                }
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

            Button btnComponentListener = newButton.GetComponent<Button>();
            if (btnComponentListener != null)
            {
                btnComponentListener.onClick.AddListener(() => SelectSkin(skinData));
            }

            buttonsInColumn++;
            if (buttonsInColumn >= maxButtonsPerColumn)
            {
                Debug.Log("changement de colonnes");
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
