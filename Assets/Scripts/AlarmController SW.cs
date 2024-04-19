using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmControllerSW : MonoBehaviour
{
    public GameObject Activated;
    public void TriggerAlarmOn()
    {
        SleepTimeout(2f);
    }

    private void SleepTimeout(float v)
    {
        throw new NotImplementedException();
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
