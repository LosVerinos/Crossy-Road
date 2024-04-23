using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkinButtonManager: MonoBehaviour
{
    public GameObject buttonPrefab; // Référence au préfabriqué de bouton
    public Transform panel; // Référence au panneau où les boutons seront instanciés
    public List<SkinData> skinDataList; // Liste des données de skin à afficher
    public float buttonSpacing = 10f; // Espacement entre les boutons

    void Start()
    {
        // Initialiser la position de départ pour le premier bouton
        float currentYPosition = 0f;

        // Créer un bouton pour chaque skin dans la liste
        foreach (SkinData skinData in skinDataList)
        {
            // Instancier un nouveau bouton à partir du préfabriqué
            GameObject newButton = Instantiate(buttonPrefab, panel);

            // Définir la position du bouton
            RectTransform buttonTransform = newButton.GetComponent<RectTransform>();
            buttonTransform.localPosition = new Vector3(buttonTransform.localPosition.x, currentYPosition, buttonTransform.localPosition.z);

            // Mettre à jour la position Y pour le prochain bouton
            currentYPosition -= buttonTransform.rect.height + buttonSpacing;

            // Récupérer le composant de texte du bouton pour afficher le nom du skin
            Text buttonText = newButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = skinData.theme; // Utiliser le thème du skinData pour le nom du bouton
            }

            // Récupérer le composant d'image du bouton pour afficher l'image du skin (si disponible)
            Image buttonImage = newButton.GetComponentInChildren<Image>();
            if (buttonImage != null && skinData.image != null)
            {
                buttonImage.sprite = skinData.image.sprite;
            }

            // Ajouter un gestionnaire de clic au bouton pour sélectionner le skin
            Button buttonComponent = newButton.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => SelectSkin(skinData));
            }
        }
    }

    void SelectSkin(SkinData skinData)
    {
        // Sélectionner le skin et effectuer les actions nécessaires
        Debug.Log("Skin selected: " + skinData.theme);
    }
}
