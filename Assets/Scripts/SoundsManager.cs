using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    private AudioSource audioSource;
    private int themeNumber;
    [SerializeField] private AudioClip[] sounds;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DetermineThemeNumber();
    }

    private void Update(){
        DetermineThemeNumber();
    }

    public void PlaySound()
    {

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.clip = sounds[themeNumber];
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource ou AudioClip non défini dans le SoundManager.");
        }
    }

    private void DetermineThemeNumber(){
        Debug.Log(GlobalVariables.theme);
        if(GlobalVariables.theme == "None"){
            themeNumber = 0;
        }
        else if(GlobalVariables.theme == "StarWars"){
            themeNumber = 1;
        }
        else if(GlobalVariables.theme == "HarryPotter"){
            themeNumber = 2;
        }
        else if(GlobalVariables.theme == "LOTR"){
            themeNumber = 3;
        }
    }
    
}
