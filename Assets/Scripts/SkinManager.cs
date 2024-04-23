using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkinButtonManager : MonoBehaviour
{
    public GameObject buttonPrefab; // Référence au préfabriqué de bouton
    public Transform panel; // Référence au panneau où les boutons seront instanciés
    public List<SkinData> skinDataList; // Liste des données de skin à afficher
    public float buttonSpacing = 10f; // Espacement entre les boutons
    public float buttonSize = 200f; // Taille des boutons carrés doublée
    public int maxButtonsPerColumn = 10; // Nombre maximum de boutons par colonne

    void Start()
    {
        // Initialiser la position de départ pour le premier bouton
        float currentXPosition = 0f;
        float currentYPosition = buttonSize / 2f; // Déplacer légèrement plus haut

        int buttonsInColumn = 0;

        // Créer un bouton pour chaque skin dans la liste
        foreach (SkinData skinData in skinDataList)
        {
            // Instancier un nouveau bouton à partir du préfabriqué
            GameObject newButton = Instantiate(buttonPrefab, panel);

            // Définir la position du bouton
            RectTransform buttonTransform = newButton.GetComponent<RectTransform>();
            buttonTransform.localPosition = new Vector3(currentXPosition, currentYPosition, buttonTransform.localPosition.z);

            // Ajuster la taille du bouton
            buttonTransform.sizeDelta = new Vector2(buttonSize, buttonSize);

            // Vérifier si le skin est déverrouillé
            if (!skinData.unlocked)
            {
                // Rendre le bouton non interactif
                Button btnComponent2 = newButton.GetComponent<Button>();
                if (btnComponent2 != null)
                {
                    btnComponent2.interactable = false;
                }

                // Griser l'image du bouton
                Image imgComponent2 = newButton.GetComponentInChildren<Image>();
                if (imgComponent2 != null)
                {
                    imgComponent2.color = Color.gray;
                }
            }

            // Mettre à jour la position Y pour le prochain bouton
            currentYPosition -= buttonSize + buttonSpacing;
            buttonsInColumn++;

            // Si le nombre maximum de boutons par colonne est atteint, passer à la colonne suivante
            if (buttonsInColumn >= maxButtonsPerColumn)
            {
                currentXPosition += buttonSize + buttonSpacing; // Déplacer vers la droite pour la prochaine colonne
                currentYPosition = buttonSize / 2f; // Réinitialiser la position Y, mais légèrement plus haut
                buttonsInColumn = 0; // Réinitialiser le compteur de boutons dans la colonne
            }

            // Récupérer le composant de texte du bouton pour afficher le nom du skin
            Text txtComponent = newButton.GetComponentInChildren<Text>();
            if (txtComponent != null)
            {
                txtComponent.text = skinData.theme; // Utiliser le thème du skinData pour le nom du bouton
            }

            // Récupérer le composant d'image du bouton pour afficher le sprite du skin (si disponible)
            Image imgComponent = newButton.GetComponentInChildren<Image>();
            if (imgComponent != null && skinData.sprite != null)
            {
                imgComponent.sprite = skinData.sprite; // Utiliser le sprite du skinData pour l'image du bouton
            }

            // Ajouter un gestionnaire de clic au bouton pour sélectionner le skin
            Button btnComponent = newButton.GetComponent<Button>();
            if (btnComponent != null)
            {
                btnComponent.onClick.AddListener(() => SelectSkin(skinData));
            }
        }
    }

    void SelectSkin(SkinData skinData)
    {
        // Sélectionner le skin et effectuer les actions nécessaires
        Debug.Log("Skin selected: " + skinData.theme);
    }
}
