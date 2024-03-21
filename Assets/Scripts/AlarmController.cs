using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmController : MonoBehaviour
{
    public GameObject Activated; // Référence à l'objet à déplacer lors du déclenchement de l'alarme

    // Méthode pour déclencher l'alarme
    public void TriggerAlarmOn()
    {
        if (Activated != null)
        {
            Activated.transform.Translate(Vector3.up * 1f);
            }
        
        else
        {
            Debug.LogError("Object to move not assigned!");
        }
    }

        
    public void TriggerAlarmOff()
    {
        if (Activated != null)
        {
            Activated.transform.Translate(Vector3.down * 1f);
            }
        
        else
        {
            Debug.LogError("Object to move not assigned!");
        }
    }
}

            