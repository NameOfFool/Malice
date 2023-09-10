using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D),typeof(Animator))]
public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float _GroundDistance = 0.05f;

    private CapsuleCollider2D _touchingCol;
    private Animator _animator;

    private RaycastHit2D[] _groundHits = new RaycastHit2D[5];

    private bool _isGrounded = true;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            _animator.SetBool(AnimatorStrings.isGrounded,value);
        }
    }

    private void Awake()
    {
        _touchingCol = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
       IsGrounded = _touchingCol.Cast(Vector2.down, castFilter,_groundHits, _GroundDistance) > 0;
    }

}
