using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "SkinData", menuName = "SkinData")]
public class SkinData : ScriptableObject
{
    public GameObject Model;
    public string theme;
    public bool unlocked;
    public Image image;
}
