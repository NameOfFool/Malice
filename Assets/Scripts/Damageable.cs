using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float _maxHP = 100;
    private float _currentHP;
    public UnityEvent<float, Vector2> damageableHit;
    private Animator animator;
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
    private bool _isAlive = true;
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
    private float timeSinceHit;
    public float invincibilityTimer = 0.25f;
    public bool isHit
    {
        get => animator.GetBool(AnimatorStrings.isHit);
        set => animator.SetBool(AnimatorStrings.isHit, value);
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        _currentHP = MaxHP;
    }
    private void Update()
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
    }
    public bool Hit(float damage, Vector2 knockback) //TODO AttackAction
    {
        if (IsAlive && !IsInvincible)
        {
            CurrentHP -= damage;
            damageableHit?.Invoke(damage, knockback);
            IsInvincible = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
