using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static bool isPlayerKilled = false;
    public static string theme;
    public static SkinData skin;
    public static bool run = false;
    public static int coins = 0;
    public static PlayerScript Player { get; set; }
    public static bool reload = false;
    public static float difficulty = 1.0f;

    void Start()
    {

        run = false;

        PlayerPrefs.SetInt("Coins", coins);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
