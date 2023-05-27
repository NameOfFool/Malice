using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEditor;
using CustomAttributes;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.InputSystem.Interactions;

public class PhysicalPlayerController : Creature
{
   private PlayerActions _actions;

  //  [CustomAttributes.ReadOnly][SerializeField] private bool IsGround;
  protected override void Awake()
    {
        base.Awake();
        _actions = new PlayerActions();
        BindInuts();
        

    }
    protected void BindInuts()
    {
        _actions.Player_Map.Jump.started += Jump;
        _actions.Player_Map.Attack.started += Attack;
    }
    protected override void Attack(InputAction.CallbackContext context)
    {
        _animator.SetTrigger("Attack");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position,AttackRange,EnemyLayer);
        foreach(Collider2D enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }
    protected override bool IsGround()
    {
       
            _boxCenter = new Vector2(_collider.bounds.center.x,_collider.bounds.center.y)+(Vector2.down*(_collider.bounds.extents.y+(GroundCheckHeight/2f)));
            _boxSize = new Vector2(_collider.bounds.size.x/2, GroundCheckHeight);
            Collider2D groundBox = Physics2D.OverlapBox(_boxCenter,_boxSize,0f,GroundLayer);
           // IsGround = groundBox != null;
            return groundBox != null;
        
    }
    
    protected void OnEnable()
    {
        _actions.Player_Map.Enable();   
    }
    protected void OnDisable()
    {
        _actions.Player_Map.Disable();
    }
    protected override IEnumerator EnableGroundCheckAfterJump()
    {
        _groundCheckEnabled = false;
        yield return _wait;
        _groundCheckEnabled = true;
    }
    protected override void Run()
    {
        _rB.velocity = new Vector2(_actions.Player_Map.Movement.ReadValue<Vector2>().x * Speed, _rB.velocity.y);
    }
    protected override void Jump(InputAction.CallbackContext context)
    {
        
        if(IsGround())
        {
            _rB.velocity += Vector2.up * JumpForce;
            _jumping = true;
            StartCoroutine(EnableGroundCheckAfterJump());
        }
            
    }
    protected override void OnDrawGizmosSelected()
    {
        if(_jumping)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireCube(_boxCenter,_boxSize);
    }
    
}
