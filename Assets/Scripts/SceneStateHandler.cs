using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneStateHandler : MonoBehaviour
{
    public int levelIndex = 0;
    public static SceneStateHandler Instance;
    [HideInInspector] public OnInGameEvent OnLevelStart;
    [HideInInspector] public OnInGameEvent OnPlayerLose;
    [HideInInspector] public OnInGameEvent OnPlayerWin;
    private bool isLose;
    private bool isWin;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnLevelStart.Invoke(levelIndex, GameEventType.LevelStart);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(levelIndex);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadNextScene()
    {
        if (SceneManager.sceneCountInBuildSettings == levelIndex + 1)
            SceneManager.LoadScene(levelIndex);
        else
            SceneManager.LoadScene(levelIndex + 1);

    }

    public void HandleLose()
    {
        if (isLose)
            return;
        isLose = true;

        OnPlayerLose.Invoke(levelIndex, GameEventType.PlayerLose);
    }

    public void HandleWin()
    {
        if (isWin)
            return;
        isWin = true;

        OnPlayerWin.Invoke(levelIndex, GameEventType.PlayerWin);
    }
}

[System.Serializable]
public class OnInGameEvent : UnityEvent<int, GameEventType> { }
