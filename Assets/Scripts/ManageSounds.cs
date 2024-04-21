using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSounds : MonoBehaviour
{
    public AudioClip[] musicList;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (musicList != null && musicList.Length > 0)
        {
            int randomIndex = Random.Range(0, musicList.Length);
            AudioClip randomClip = musicList[randomIndex];

            audioSource.clip = randomClip;

            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Aucune liste de musique n'a été fournie.");
        }
    }
}
