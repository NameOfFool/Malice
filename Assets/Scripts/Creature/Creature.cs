using CustomAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Creature:MonoBehaviour
{
    [Header("Creature Properties")]
    [SerializeField] protected float MaxHP = 100f;
    [SerializeField][ReadOnly] protected float CurrentHP = 100f;
    [SerializeField] protected float DefaultSpeed = 10f;
    [SerializeField] protected float DefaultAttackDamage = 5f;
    [SerializeField] protected float DefaultAttackRange = 1.63f;
    [SerializeField] protected float DefaultJumpForce = 10f;
    [SerializeField][Range(1f, 5f)] protected  float JumpFallGravityMultiplier = 3;
    [SerializeField] protected LayerMask EnemyLayer;
    [Header("Ground Check Properties")]
    [SerializeField] protected  LayerMask GroundLayer;
    [SerializeField] protected  float GroundCheckHeight;
    [SerializeField] protected float GhargeJumpPower;
    [SerializeField] protected float DisableCGTime;


    protected SpriteRenderer _sR;
    protected Animator _animator;
    protected BoxCollider2D _collider;
    protected Rigidbody2D _rB;

    protected Vector2 _moveInput;
    protected Vector2 _boxCenter;
    protected Vector2 _boxSize;
    protected bool _jumping;
    protected bool _groundCheckEnabled = true;
    protected WaitForSeconds _wait;
    protected float _initialGravityScale;


    protected ActiveItem _activeItem;
    protected virtual void Awake()
    {
        AttackAction a = new AttackAction
        {
            damageType = DamageType.Stabbing,
            AttackValue = 10
        };
        _activeItem = new ActiveItem
        {
            Name ="Def",
            Description = "Desc",
            mainAction = a,
            alternativeAction = new Action()
        };
        _sR = GetComponent<SpriteRenderer>();

        _rB = GetComponent<Rigidbody2D>();

        _initialGravityScale = _rB.gravityScale;

        _wait = new WaitForSeconds(DisableCGTime);

        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
       
    }
    public virtual void MainAction(InputAction.CallbackContext context)
    {
        _activeItem.mainAction.DoAction(this);
    }
    public virtual void Attack(AttackAction action)
    {
        _animator.SetTrigger("Attack");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, DefaultAttackRange, EnemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Creature>().GetDamage(DefaultAttackDamage+action.AttackValue,action.damageType);
        }
    }
    public virtual void GetDamage(float damageValue, DamageType damageType)
    {
        CurrentHP -= damageValue;
        if(CurrentHP <= 0)
        {
            Destroy(_collider.gameObject);
        }
    }
    protected virtual bool IsGround()
    {

        _boxCenter = new Vector2(_collider.bounds.center.x, _collider.bounds.center.y) + (Vector2.down * (_collider.bounds.extents.y + (GroundCheckHeight / 2f)));
        _boxSize = new Vector2(_collider.bounds.size.x / 2, GroundCheckHeight);
        Collider2D groundBox = Physics2D.OverlapBox(_boxCenter, _boxSize, 0f, GroundLayer);
        // IsGround = groundBox != null;
        return groundBox != null;

    }

 
    protected virtual IEnumerator EnableGroundCheckAfterJump()
    {
        _groundCheckEnabled = false;
        yield return _wait;
        _groundCheckEnabled = true;
    }
    protected virtual void FixedUpdate()
    {
        Run();
        HandleGravity();
        Flip();
    }
    protected virtual void HandleGravity()
    {
        if (_groundCheckEnabled && IsGround())
        {
            _jumping = false;
        }
        else if (_jumping && _rB.velocity.y < 0)
        {
            _rB.gravityScale = _initialGravityScale * JumpFallGravityMultiplier;
        }
        else
        {
            _rB.gravityScale = _initialGravityScale;
        }
    }
    protected virtual void Run()
    {
        Vector2 direction = new Vector2(1f,0f);
        _rB.velocity = new Vector2(direction.x * DefaultSpeed, _rB.velocity.y);
        
        
    }
    protected virtual void Flip()
    {
        if (Mathf.Abs(_rB.velocity.x) >= 0.1f)
            _sR.flipX = _rB.velocity.x < 0f;
    }
    protected virtual void Jump(InputAction.CallbackContext context)
    {

        if (IsGround())
        {
            _rB.velocity += Vector2.up * DefaultJumpForce;
            _jumping = true;
            StartCoroutine(EnableGroundCheckAfterJump());
        }

    }
    protected virtual void OnDrawGizmosSelected()
    {
        if (_jumping)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireCube(_boxCenter, _boxSize);
    }
}
