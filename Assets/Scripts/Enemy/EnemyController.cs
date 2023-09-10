using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class EnemyController : MonoBehaviour
{
    public float DefaultSpeed = 10f;
    public float DefaultAttackDamage = 5f;
    public float DefaultAttackRange = 1.63f;
    public float DefaultJumpForce = 10f;

    Rigidbody2D rb;

    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector;

    public WalkableDirection WalkDirection
    {
        get => _walkDirection;
        set
        {
            if(_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if(value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(DefaultSpeed * Vector2.right.x, rb.velocity.y);
    }

}
