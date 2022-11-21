using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisionDetection : MonoBehaviour
{
    public bool canInvokeWin;

    private void OnCollisionEnter(Collision other)
    {
        TrashCan trashCan;
        if (other.gameObject.TryGetComponent<TrashCan>(out trashCan))
        {
            SFXManager.Instance.PlaySound(0);
        }

        LoseSurface loseSurface;
        if (other.gameObject.TryGetComponent<LoseSurface>(out loseSurface))
        {
            SceneStateHandler.Instance.HandleLose();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        WinSurface winSurface;
        if (other.gameObject.TryGetComponent<WinSurface>(out winSurface) && canInvokeWin)
        {
            SceneStateHandler.Instance.HandleWin();
        }
    }
}