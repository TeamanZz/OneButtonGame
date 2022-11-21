using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float characterRunSpeed;
    [SerializeField] private float characterMaxRunSpeed;
    [SerializeField] private List<Rigidbody> ragdollRBList = new List<Rigidbody>();
    [SerializeField] private List<Collider> ragdollCollidersList = new List<Collider>();

    private CharacterState characterState;
    private Rigidbody characterRB;
    private Collider defaultCollider;
    private Animator animator;
    private bool canRun = false;
    private float interpolatedRunningSpeed;

    private void Awake()
    {
        Initialize();
        DisableRagdollComponents();
    }

    private void Update()
    {
        HandleInput();
        HandleBlendTreeState();
    }

    private void FixedUpdate()
    {
        HandleRunningVelocity();
    }

    public void HandleFreeFall()
    {
        SaveInterpolatedRunningSpeed();
        EnableRagdoll();
        AddForceOnFreeFall();
        SetNewState(CharacterState.Ragdoll);
    }

    private void AddForceOnFreeFall()
    {
        foreach (var item in ragdollRBList)
        {
            item.AddForce(new Vector3(2, 0, 0) * jumpForce * interpolatedRunningSpeed, ForceMode.Impulse);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (characterState == CharacterState.Idle)
            {
                HandleStartRunning();
            }
            else if (characterState == CharacterState.Running)
            {
                HandleJump();
            }
        }
    }

    private void HandleStartRunning()
    {
        PlayRunningAnimation();
        EnableRunning();
        SetNewState(CharacterState.Running);
    }

    private void HandleJump()
    {
        SaveInterpolatedRunningSpeed();
        EnableRagdoll();
        AddJumpForce();
        SetNewState(CharacterState.Ragdoll);
    }

    private void HandleRunningVelocity()
    {
        if (canRun)
            characterRB.AddForce(Vector3.right * characterRunSpeed, ForceMode.Impulse);
        if (characterRB.velocity.x > characterMaxRunSpeed)
            characterRB.velocity = new Vector3(characterMaxRunSpeed, characterRB.velocity.y, characterRB.velocity.z);
    }

    private void EnableRunning()
    {
        canRun = true;
    }

    private void SaveInterpolatedRunningSpeed()
    {
        interpolatedRunningSpeed = characterRB.velocity.x / characterMaxRunSpeed;
    }

    private void PlayRunningAnimation()
    {
        animator.SetBool("IsRunning", true);
    }

    private void AddJumpForce()
    {
        foreach (var item in ragdollRBList)
        {
            item.AddForce(new Vector3(2, 2, 0) * jumpForce * interpolatedRunningSpeed, ForceMode.Impulse);
        }
    }

    private void DisableRagdollComponents()
    {
        foreach (var item in ragdollRBList)
        {
            item.isKinematic = true;
        }

        foreach (var item in ragdollCollidersList)
        {
            item.enabled = false;
        }
    }

    private void EnableRagdoll()
    {
        defaultCollider.enabled = false;
        foreach (var item in ragdollRBList)
        {
            item.isKinematic = false;
        }

        foreach (var item in ragdollCollidersList)
        {
            item.enabled = true;
        }

        animator.enabled = false;
        characterRB.constraints = RigidbodyConstraints.None;
        characterRB.isKinematic = true;
    }

    private void HandleBlendTreeState()
    {
        if (canRun)
        {
            float value = characterRB.velocity.x / characterMaxRunSpeed;
            animator.SetFloat("Blend", value);
        }
    }

    private void SetNewState(CharacterState newState)
    {
        characterState = newState;
    }

    private void Initialize()
    {
        characterRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        defaultCollider = GetComponent<Collider>();
    }
}

enum CharacterState
{
    Idle,
    Running,
    Ragdoll
}
