using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static bool isPlayerKilled = false;
    public static bool run = false;
    public static PlayerScript Player { get; set; }

    void Start()
    {
        run = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
