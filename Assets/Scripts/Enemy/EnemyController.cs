using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class EnemyController : MonoBehaviour
{
    public float DefaultSpeed = 5f;
    public float DefaultJumpForce = 10f;

    private Rigidbody2D rb;
    [SerializeField] private HitBox hitbox;

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
        get=>CanMove?DefaultSpeed * walkDirectionVector.x:0;
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
    }
    private void FixedUpdate()
    {
        if (touchingDirections.IsOnWall && touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
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
            Debug.Log("Unexpected walkable direction");
        }
    }

}
