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

    public void ChangeLightColor(Color color)
    {
        if (lightComponent != null)
        {
            lightComponent.color = color;
        }
        else
        {
            Debug.LogError("Composant de lumière non trouvé.");
        }
    }

    public void ChangeLightIntensity(float intensity)
    {
        if (lightComponent != null)
        {
            lightComponent.intensity = intensity;
        }
        else
        {
            Debug.LogError("Composant de lumière non trouvé.");
        }
    }
}
