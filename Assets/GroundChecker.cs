using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private void OnTriggerExit(Collider other)
    {
        JumpSurface jumpSurface;
        if (other.TryGetComponent<JumpSurface>(out jumpSurface))
            playerController.HandleFreeFall();
    }
}