using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEditor;
using CustomAttributes;
using UnityEngine.InputSystem;
using System.Linq;
using TMPro;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Properties")]
    public float DefaultSpeed = 10f;
    public float DefaultAttackDamage = 5f;
    public float DefaultAttackRange = 1.63f;
    public float DefaultJumpForce = 10f;
    public PlayerHealth health;
    public float DefaultAirWalkSpeed = 7f;
    [Range(1f, 5f)] public float JumpFallGravityMultiplier = 3;

    [Header("Ground Check Properties")]
    public float GhargeJumpPower;
    public float DisableCGTime;

    private SpriteRenderer _sr;
    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 moveInput;
    private TouchingDirections touchingDirections;
    private Damageable damageable;

    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }
    private bool _isMoving = false;


    public bool isMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimatorStrings.isMoving, value);
        }
    }

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (isMoving && !touchingDirections.IsOnWall && !touchingDirections.IsOnCeiling)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        return DefaultSpeed;
                    }
                    else
                    {
                        return DefaultAirWalkSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimatorStrings.canMove);
        }
    }
    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimatorStrings.isAlive);
        }
    }
    public void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();

        damageable = GetComponent<Damageable>();

        touchingDirections = GetComponent<TouchingDirections>();

        animator = GetComponent<Animator>();

        Physics2D.IgnoreLayerCollision(6, 7);

    }
    public void FixedUpdate()
    {
        
        rb.velocity = new(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        animator.SetFloat(AnimatorStrings.yVelocity, rb.velocity.y);
    }
    private void Flip()
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if(IsAlive)
        {
            isMoving = moveInput != Vector2.zero;
            Flip();
        }
        else
            isMoving = false;
    }
    public void onJump(InputAction.CallbackContext context)
    {
        //TODO check HP
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimatorStrings.jumpTrigger);
            rb.velocity += Vector2.up * DefaultJumpForce;
        }
    }
    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimatorStrings.attackTrigger);
        }
    }
    public void onHit(float damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
