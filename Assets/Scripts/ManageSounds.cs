using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSounds : MonoBehaviour
{
    [Header("Music Clips")]
    public AudioClip[] musicList;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (musicList == null || musicList.Length == 0)
        {
            Debug.LogWarning("Aucune liste de musique n'a été fournie.");
            return;
        }

        PlayRandomMusic();
    }

    private void PlayRandomMusic()
    {
        var randomIndex = Random.Range(0, musicList.Length);
        var randomClip = musicList[randomIndex];

        audioSource.clip = randomClip;
        audioSource.Play();
    }
}
