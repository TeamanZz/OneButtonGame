using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> soundsList = new List<AudioClip>();
    public static SFXManager Instance;
    private bool isPlayed;
    private void Awake()
    {
        Instance = this;
    }

    public void PlaySound(int index)
    {
        if (!isPlayed)
        {
            audioSource.PlayOneShot(soundsList[index]);
            isPlayed = true;
        }
    }
}
