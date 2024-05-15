using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSounds : MonoBehaviour
{
    public AudioClip[] musicList;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (musicList != null && musicList.Length > 0)
        {
            var randomIndex = Random.Range(0, musicList.Length);
            var randomClip = musicList[randomIndex];

            audioSource.clip = randomClip;

            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Aucune liste de musique n'a été fournie.");
        }
        SoundsManager ambiance = GetComponentInChildren<SoundsManager>();
        ambiance.PlaySound();
    }
}