using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShop_StartMenu : MonoBehaviour
{
    public GameObject Panel;
    public GameObject StartMenu;

    public void onClick()
    {
        StartMenu.SetActive(false);
        Panel.SetActive(true);
    }
}
