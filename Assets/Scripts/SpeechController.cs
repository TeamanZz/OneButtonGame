using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechController : MonoBehaviour
{
    [SerializeField] private float speechDelay;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<LevelSpeeches> levelsSpeechesList = new List<LevelSpeeches>();

    private SceneStateHandler sceneStateHandler;

    public AudioClip lastClip;

    private void Awake()
    {
        sceneStateHandler = GetComponent<SceneStateHandler>();
        sceneStateHandler.OnLevelStart.AddListener(CheckOnSpeech);
        sceneStateHandler.OnPlayerLose.AddListener(CheckOnSpeech);
        sceneStateHandler.OnPlayerWin.AddListener(CheckOnSpeech);
    }

    public void CheckOnSpeech(int levelIndex, GameEventType eventType)
    {
        SpeechEvent speechEvent = levelsSpeechesList[levelIndex].levelSpeeches.Find(x => x.eventType == eventType);
        if (speechEvent != null)
            PlaySpeech(speechEvent.audioClip, true);
    }

    public void PlaySpeech(AudioClip audioClip, bool delayed)
    {
        StartCoroutine(IEPlaySpeech(audioClip, delayed));
    }

    private IEnumerator IEPlaySpeech(AudioClip audioClip, bool delayed)
    {
        lastClip = audioClip;
        yield return new WaitForSeconds(speechDelay);
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }

    public float GetCurrentSpeechLength()
    {
        return (lastClip.length);
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