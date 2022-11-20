using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechController : MonoBehaviour
{
    public AudioSource audioSource;
    public int speechIndex;
    public List<AudioClip> speechsList = new List<AudioClip>();

    private void Start()
    {
        Invoke("PlaySpeech", 2);
    }

    public void PlaySpeech()
    {
        audioSource.PlayOneShot(speechsList[speechIndex]);
    }
}
