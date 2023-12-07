using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] protected float _maxHP = 100;
    protected float _currentHP;
    public UnityEvent<float, Vector2> damageableHit;
    protected Rigidbody2D rb;
    protected Vector3 spawnPoint;
    protected Animator animator;
    public float MaxHP { get => _maxHP; set => _maxHP = value; }
    public float CurrentHP
    {
        get => _currentHP;
        set
        {
            _currentHP = value;
            if (_currentHP <= 0)
            {
                IsAlive = false;
            }
        }
    }
    protected bool _isAlive = true;
    public bool IsAlive
    {
        get => _isAlive;
        set
        {
            _isAlive = value;
            animator.SetBool(AnimatorStrings.isAlive, value);
        }
    }
    public bool IsInvincible
    {
        get;
        set;
    }
    protected float timeSinceHit;
    public float invincibilityTimer = 0.25f;
    public bool isHit
    {
        get => animator.GetBool(AnimatorStrings.isHit);
        set => animator.SetBool(AnimatorStrings.isHit, value);
    }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _currentHP = MaxHP;
        spawnPoint = transform.position;
    }
    protected virtual void Update()
    {
        if (IsInvincible)
        {
            if (timeSinceHit > invincibilityTimer)
            {
                IsInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
        if(transform.position.y < -2)
        {
            Fall();
        }
    }
    public bool Hit(float damage, Vector2 knockback, Vector3 sourcePosition) //TODO AttackAction
    {
        if (IsAlive && !IsInvincible)
        {
            CurrentHP -= damage;
            knockback = sourcePosition.x < transform.position.x ? knockback * Vector2.left : knockback;
            damageableHit?.Invoke(damage, knockback);
            IsInvincible = true;
            animator.SetTrigger(AnimatorStrings.hit);
            return true;
        }
        else
        {
            return false;
        }
    }
    public virtual void onHit(float damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    public virtual void Fall()
    {
        transform.position = spawnPoint;
    }
}
