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

    private Rigidbody characterRB;
    private Collider defaultCollider;
    private Animator animator;
    private bool canRun = false;
    private int stateIndex;

    private void Awake()
    {
        characterRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        defaultCollider = GetComponent<Collider>();
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

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (stateIndex == 0)
            {
                HandleStartRunning();
            }
            else if (stateIndex == 1)
            {
                HandleJump();
            }
        }
    }

    public void HandleStartRunning()
    {
        PlayRunningAnimation();
        EnableRunning();
        SetNewStateIndex(1);
    }

    private void HandleJump()
    {
        EnableRagdoll();
        AddJumpForce();
        SetNewStateIndex(2);
    }

    private void HandleRunningVelocity()
    {
        if (canRun)
            characterRB.AddForce(Vector3.right * characterRunSpeed, ForceMode.Impulse);
        if (characterRB.velocity.x > characterMaxRunSpeed)
            characterRB.velocity = new Vector3(characterMaxRunSpeed, characterRB.velocity.y, characterRB.velocity.z);
    }

    public void EnableRunning()
    {
        canRun = true;
    }

    public void PlayRunningAnimation()
    {
        animator.SetBool("IsRunning", true);
    }

    private void AddJumpForce()
    {
        foreach (var item in ragdollRBList)
        {
            item.AddForce(new Vector3(2, 2, 0) * jumpForce, ForceMode.Impulse);
        }
    }

    private void DisableRagdollComponents()
    {
        foreach (var item in ragdollCollidersList)
        {
            item.enabled = false;
        }

        foreach (var item in ragdollRBList)
        {
            item.isKinematic = true;
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
        characterRB.useGravity = false;
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

    private void SetNewStateIndex(int newIndex)
    {
        stateIndex = newIndex;
    }

}