using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int speechIndex;
    [SerializeField] private List<AudioClip> speechsList = new List<AudioClip>();

    private void Start()
    {
        Invoke("PlaySpeech", 2);
    }

    public void PlaySpeech()
    {
        audioSource.PlayOneShot(speechsList[speechIndex]);
    }
}
