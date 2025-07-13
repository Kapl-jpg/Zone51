using System;
using System.Collections;
using UnityEngine;

public class LiftMovement : Subscriber
{
    [SerializeField] private Transform upPoint;
    [SerializeField] private Transform downPoint;
    [SerializeField] private AudioSource audioElevator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pauseTime;
    [SerializeField] private Collider liftCollider;
    
    private Transform _player;
    private bool _move;
    private bool _moveUp;
    private bool _pause;

    [Event("CloseDoorLift")]
    private void MoveLift()
    {
        audioElevator.Play();
        if (!_move)
            CloseDoor();
    }

    [Event("Landing")]
    private void ResetJump()
    {
        _pause = true;
    }

    [Event("MoveLift")]
    private void Movement()
    {
        StartCoroutine(Move());
    }
    
    private void CloseDoor()
    {
        liftCollider.enabled = true;
        _move = true;
        EventManager.Publish(_moveUp ? "CloseBottomDoor" : "CloseTopDoor");
    }
    
    private IEnumerator Move()
    {
        var endPoint = _moveUp ? upPoint.position : downPoint.position;
        
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
                yield return new WaitForSeconds(pauseTime);
                _pause = false;
            }
        }

        liftCollider.enabled = false;
        EventManager.Publish(_moveUp ? "OpenTopDoor" : "OpenBottomDoor");
        
        _moveUp = !_moveUp;
        _move = false;
    }
}
