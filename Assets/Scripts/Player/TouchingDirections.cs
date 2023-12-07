using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Animator))]
public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;

    public float GroundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float CeilingDistance = 0.05f;

    private Collider2D touchingCol;
    private Animator animator;

    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] cliffHits = new RaycastHit2D[5];
    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private bool _isGrounded = true;
    private bool _isOnWall;
    private bool _isOnCeiling;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimatorStrings.isGrounded, value);
        }
    }
    public bool IsOnWall
    {
        get => _isOnWall;
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimatorStrings.isOnWall, value);
        }
    }

    public bool IsOnCeiling
    {
        get => _isOnCeiling;
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimatorStrings.isOnCeiling, value);
        }
    }
    private void Awake()
    {
        touchingCol = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, GroundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, CeilingDistance) > 0;
    }

}
