using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "SkinData", menuName = "SkinData")]
public class SkinData : ScriptableObject
{
    public GameObject Model;
    public string theme;
    public bool unlocked;
    public Sprite sprite;
    public bool selected;
}