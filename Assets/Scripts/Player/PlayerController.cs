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
    private Animator _animator;
    private BoxCollider2D _collider;
    private Rigidbody2D _rb;

    private Vector2 _moveInput;
    TouchingDirections _touchingDirections;

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
            _animator.SetBool(AnimatorStrings.isMoving, value);
        }
    }

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (isMoving)
                {
                    if (_touchingDirections.IsGrounded)
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
            return _animator.GetBool(AnimatorStrings.canMove);
        }
    }
    public void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();

        _rb = GetComponent<Rigidbody2D>();

        _collider = GetComponent<BoxCollider2D>();

        _touchingDirections = GetComponent<TouchingDirections>();

        _animator = GetComponent<Animator>();

        Physics2D.IgnoreLayerCollision(6, 7);

    }
    public void FixedUpdate()
    {
        _rb.velocity = new(_moveInput.x * CurrentMoveSpeed, _rb.velocity.y);
        _animator.SetFloat(AnimatorStrings.yVelocity, _rb.velocity.y);
        Flip();
    }
    private void Flip()
    {
        if (_moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (_moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
    public void onMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        isMoving = _moveInput != Vector2.zero;
    }
    public void onJump(InputAction.CallbackContext context)
    {
        //TODO check HP
        if (context.started && _touchingDirections.IsGrounded && CanMove)
        {
            _animator.SetTrigger(AnimatorStrings.jumpTrigger);
            _rb.velocity += Vector2.up * DefaultJumpForce;
        }
    }
    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetTrigger(AnimatorStrings.attackTrigger);
        }
    }
}
