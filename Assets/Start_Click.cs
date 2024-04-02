using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Click : MonoBehaviour
{

    public GameObject Panel;

    public void OnButtonClick()
    {
        Panel.SetActive(false);
    }
}
