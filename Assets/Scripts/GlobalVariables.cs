using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static bool isPlayerKilled = false;
    public static bool run = false;
    public static int coins;
    public static PlayerScript Player { get; set; }

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
