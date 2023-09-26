using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public float DefaultSpeed = 5f;
    public float DefaultJumpForce = 10f;
    public float WalkStopRate = 0.05f;

    private Rigidbody2D rb;
    [SerializeField] private DetectionZone hitbox;

    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection;
    private TouchingDirections touchingDirections;
    private Animator animator;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get => _walkDirection;
        set
        {
            if (_walkDirection != value)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
                _walkDirection = value;
            }
        }
    }
    private bool _hasTarget;
    public bool HasTarget
    {
        get => _hasTarget;
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimatorStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get => animator.GetBool(AnimatorStrings.canMove);
    }
    public float CurrentMoveSpeed
    {
        get => CanMove ? DefaultSpeed * walkDirectionVector.x : Mathf.Lerp(rb.velocity.x, 0, WalkStopRate);
    }
    public bool LockVelocity { get => animator.GetBool(AnimatorStrings.lockVelocity); }
    public float AttackCD //CoolDown
    {
        get => animator.GetFloat(AnimatorStrings.attackCD);
        set => animator.SetFloat(AnimatorStrings.attackCD, Mathf.Max(0,value));
    } 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }
    private void Update()
    {
        HasTarget = hitbox.detectedColliders.Count > 0;
        AttackCD -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if ((touchingDirections.IsOnWall) && touchingDirections.IsGrounded && CanMove)
        {
            FlipDirection();
        }
        if (!LockVelocity)
            rb.velocity = new Vector2(CurrentMoveSpeed, rb.velocity.y);
    }
    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Unexpected walkable direction");
        }
    }
    public void onNoGroundDetected()
    {
        if(touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }

}
