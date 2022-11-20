using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody characterRB;
    private Animator animator;
    private bool canRun = false;

    [SerializeField] private float characterRunSpeed;
    [SerializeField] private float characterMaxRunSpeed;

    private void Awake()
    {
        characterRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            HandleStartRunning();

    }

    private void FixedUpdate()
    {
        HandleRunning();
    }

    private void HandleRunning()
    {
        if (canRun)
            characterRB.AddForce(Vector3.right * characterRunSpeed, ForceMode.Impulse);
        if (characterRB.velocity.x > characterMaxRunSpeed)
            characterRB.velocity = new Vector3(characterMaxRunSpeed, characterRB.velocity.y, characterRB.velocity.z);
    }

    public void HandleStartRunning()
    {
        PlayRunningAnimation();
        MoveCharacterForward();
    }

    public void MoveCharacterForward()
    {
        canRun = true;
    }

    public void PlayRunningAnimation()
    {
        animator.SetBool("IsRunning", true);
    }

}