using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneStateHandler : MonoBehaviour
{
    public int levelIndex = 0;
    public OnInGameEvent OnLevelStart;

    private void Start()
    {
        OnLevelStart.Invoke(levelIndex, GameEventType.LevelStart);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(0);
        }
    }

}

[System.Serializable]
public class OnInGameEvent : UnityEvent<int, GameEventType> { }
