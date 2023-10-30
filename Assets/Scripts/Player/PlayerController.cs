using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("Player Properties")]
    public float DefaultSpeed = 10f;
    public float DefaultJumpForce = 10f;//TODO Make sword range attack and sword item
    public float DefaultAirWalkSpeed = 7f;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private TouchingDirections touchingDirections;

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
                        if (dash)
                        {
                            return DefaultSpeed * 2;
                        }
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
        get => animator.GetBool(AnimatorStrings.canMove);
        set => animator.SetBool(AnimatorStrings.canMove, value);
    }
    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimatorStrings.isAlive);
        }
    }

    public bool LockVelocity
    {
        get => animator.GetBool(AnimatorStrings.lockVelocity);
        set => animator.SetBool(AnimatorStrings.lockVelocity, value);
    }
    public bool dash { get => animator.GetBool(AnimatorStrings.dash); }

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        touchingDirections = GetComponent<TouchingDirections>();

        animator = GetComponent<Animator>();

        Physics2D.IgnoreLayerCollision(6, 7);

    }
    public void FixedUpdate()
    {
        if (!LockVelocity)
        {
            rb.velocity = new(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }
        else if(dash)
        {
            float direction = IsFacingRight?1:-1;
            rb.velocity = new(direction * DefaultSpeed, 0);
        }
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
        if (!dash)
        {
            moveInput = context.ReadValue<Vector2>();
            Debug.Log(moveInput);
        }
        if (IsAlive)
        {
            isMoving = moveInput != Vector2.zero;
            Flip();
        }
        else
            isMoving = false;
    }
    public void onJump(InputAction.CallbackContext context)
    {
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
    public void onPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
    }
    public void onDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LockVelocity = true;
            animator.SetBool(AnimatorStrings.dash, true);
        }
    }
}
