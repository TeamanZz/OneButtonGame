using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<LevelSpeeches> levelsSpeechesList = new List<LevelSpeeches>();

    private SceneStateHandler sceneStateHandler;

    private void Awake()
    {
        sceneStateHandler = GetComponent<SceneStateHandler>();
        sceneStateHandler.OnLevelStart.AddListener(CheckOnSpeech);
    }

    public void CheckOnSpeech(int levelIndex, GameEventType eventType)
    {
        SpeechEvent speechEvent = levelsSpeechesList[levelIndex].levelSpeeches.Find(x => x.eventType == eventType);
        PlaySpeech(speechEvent.audioClip);
    }

    public void PlaySpeech(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}

[System.Serializable]
public class LevelSpeeches
{
    public List<SpeechEvent> levelSpeeches = new List<SpeechEvent>();
}

[System.Serializable]
public class SpeechEvent
{
    public GameEventType eventType;
    public AudioClip audioClip;
}

public enum GameEventType
{
    LevelStart,
    LevelRestart,
    PlayerWin,
    PlayerLose,
}