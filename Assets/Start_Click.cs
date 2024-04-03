using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Click : MonoBehaviour
{

    public GameObject Panel;
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Panel4;
    public GameObject score;
    public GameObject buttonPause;

    public void OnButtonClick()
    {
        Panel.SetActive(false);
        Panel2.SetActive(true);
        Panel3.SetActive(true);
        Panel4.SetActive(false);
        score.SetActive(true);
        buttonPause.SetActive(true);
    }
}
