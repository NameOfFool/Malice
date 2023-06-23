using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Creature
{
    [Header("Enemy Controller")]
    [SerializeField] protected Transform pointA;
    [SerializeField] protected Transform pointB;

    private Vector3 _currentTarget;
    protected override void Awake()
    {
        base.Awake();
        _currentTarget = pointA.position;
    }

    protected override void FixedUpdate()
    {
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            _rB.velocity = new Vector2(0,_rB.velocity.y);
            return;
        }
        Run();
    }
    protected override void Run()
    {
        if(_currentTarget == pointA.position)
        {
            _sR.flipX = true;
        }
        else
        {
            _sR.flipX = false;
        }
        if(transform.position.x <= pointA.position.x) 
        {
            _currentTarget = pointB.position;
            _animator.SetTrigger("Idle");

        }
        else if (transform.position.x >= pointB.position.x)
        { 
            _currentTarget = pointA.position;
            _animator.SetTrigger("Idle");
        }
        Vector2 direction = _currentTarget - transform.position;
        _rB.velocity = new Vector2(Mathf.Sign(direction.x) * DefaultSpeed, _rB.velocity.y);
    }
}
