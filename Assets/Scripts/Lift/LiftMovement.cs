using System.Collections;
using UnityEngine;

public class LiftMovement : Subscriber
{
    [SerializeField] private Transform upPoint;
    [SerializeField] private Transform downPoint;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pauseTime;
   
    private Transform _player;
    private bool _move;
    private bool _moveUp;
    private bool _pause;
    
    [Event("MoveLift")]
    private void MoveLift()
    {
        if(!_move)
            StartCoroutine(Move());
    }

    [Event("ResetJump")]
    private void ResetJump()
    {
        _pause = true;
    }
    
    private IEnumerator Move()
    {
        _move = true;
        var endPoint = _moveUp?upPoint.position: downPoint.position;
        while (transform.position != endPoint)
        {
            if (!_pause)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, endPoint, moveSpeed * Time.fixedDeltaTime);
                yield return null;
            }
            else
            {
                yield return new  WaitForSeconds(pauseTime);
                _pause = false;
            }
        }
        
        _moveUp = !_moveUp;
        _move = false;
    }
}
