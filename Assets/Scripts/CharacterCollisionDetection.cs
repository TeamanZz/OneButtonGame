using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisionDetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        TrashCan trashCan;
        if (other.gameObject.TryGetComponent<TrashCan>(out trashCan))
        {
            SFXManager.Instance.PlaySound(0);
        }
    }
}
