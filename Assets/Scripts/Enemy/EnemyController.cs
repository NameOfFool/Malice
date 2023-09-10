using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Controller")]
    
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    private Animator animator;
    private SpriteRenderer sR;

    private Vector3 _currentTarget;
    
    private bool _isWaiting = false;
    public  void Awake()
    {
        _currentTarget = pointA.position;
        
    }

    public void FixedUpdate()
    {
        if (!_isWaiting)
            Patrol();
    }
    public async void Patrol()
    {
        if (Mathf.Abs(_currentTarget.x-transform.position.x)<=sR.size.x/2)
        {
            if (_currentTarget == pointB.position || _currentTarget == pointA.position)
            {
                await Wait(1);
            }
        }
        Run();
    }
    private void ChangeTarget()
    {
        if (_currentTarget == pointB.position)
            _currentTarget = pointA.position;
        else if (_currentTarget == pointA.position)
            _currentTarget = pointB.position;
    }
    public void Run()
    {
        if(!animator.GetBool("isRunning"))
        {
            animator.SetBool("isRunning", true);
        }
        Vector2 direction = _currentTarget - transform.position;
    }
    /// <summary>
    /// The object waits the specified number of seconds, then changes the target and start running 
    /// </summary>
    /// <param name="seconds">Specified number of second</param>
    private async Task<bool> Wait(int seconds)
    {
       /* _moveInput.x = 0;
        animator.SetBool("isRunning", false);
        _isWaiting = true;
        await Task.Delay(seconds * 1000);
        ChangeTarget();
        animator.SetBool("isRunning", true);
        _isWaiting = false;*/
        return true;
    }
}
