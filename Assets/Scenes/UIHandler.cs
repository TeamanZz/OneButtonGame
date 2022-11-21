using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject loseScreen;
    public GameObject winScreen;

    private SceneStateHandler sceneStateHandler;
    private SpeechController speechController;

    private void Awake()
    {
        sceneStateHandler = GetComponent<SceneStateHandler>();
        speechController = GetComponent<SpeechController>();
        sceneStateHandler.OnPlayerLose.AddListener(EnableLoseScreen);
        sceneStateHandler.OnPlayerWin.AddListener(EnableWinScreen);
    }

    public void EnableLoseScreen(int levelIndex, GameEventType eventType)
    {
        StartCoroutine(IEEnableLoseScreen());
    }

    private IEnumerator IEEnableLoseScreen()
    {
        yield return new WaitForSeconds(0.5f);
        float delay = speechController.GetCurrentSpeechLength() + 0.5f;
        yield return new WaitForSeconds(delay);
        loseScreen.SetActive(true);
    }

    public void EnableWinScreen(int levelIndex, GameEventType eventType)
    {
        StartCoroutine(IEEnableWinScreen());
    }

    private IEnumerator IEEnableWinScreen()
    {
        yield return new WaitForSeconds(0.5f);
        float delay = speechController.GetCurrentSpeechLength() + 0.5f;
        Debug.Log(delay);
        yield return new WaitForSeconds(delay);
        winScreen.SetActive(true);
    }
}