using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_startMenu : MonoBehaviour
{
    public GameObject Panel;
    public GameObject StartMenu;

    public void OnClick()
    {
        StartMenu.SetActive(false);
        Panel.SetActive(true);
    }
}
