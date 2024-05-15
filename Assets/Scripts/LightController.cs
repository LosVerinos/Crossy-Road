using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private Light lightComponent;

    private void Start()
    {
        // Récupère le composant de lumière attaché à cet objet
        lightComponent = GetComponent<Light>();

        if (lightComponent == null)
        {
            Debug.LogError("Composant de lumière non trouvé.");
        }
    }

    // Méthode pour vérifier si le composant de lumière est présent
    private bool IsLightComponentValid()
    {
        if (lightComponent == null)
        {
            Debug.LogWarning("Tentative de modification de la lumière sans composant de lumière valide.");
            return false;
        }
        return true;
    }

    // Méthode pour changer la couleur de la lumière
    public void ChangeLightColor(Color color)
    {
        if (IsLightComponentValid())
        {
            lightComponent.color = color;
        }
    }

    // Méthode pour changer l'intensité de la lumière
    public void ChangeLightIntensity(float intensity)
    {
        if (IsLightComponentValid())
        {
            lightComponent.intensity = intensity;
        }
    }
}
