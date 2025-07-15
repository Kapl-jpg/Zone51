using System;
using System.Collections;
using UnityEngine;

public class LiftMovement : Subscriber
{
    [SerializeField] private Transform upPoint;
    [SerializeField] private Transform downPoint;
    [SerializeField] private AudioSource audioMovementElevator;
    [SerializeField] private AudioSource audioStartElevator;
    [SerializeField] private AudioSource audioFinishElevator;
    [SerializeField] private AudioSource audioStopElevator; // ???
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pauseTime;
    [SerializeField] private Collider liftCollider;
    
    private Transform _player;
    private bool _move;
    private bool _moveUp;
    private bool _pause;
    private bool activeAudioFinish = true;
    private bool activeAudioStart = true;

    [Event("CloseDoorLift")]
    private void MoveLift()
    {
        
        //print("Move");
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
        //print("Move");
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
        _pause = false;
        var endPoint = _moveUp ? upPoint.position : downPoint.position;
        if (!audioStartElevator.isPlaying && activeAudioStart)
        {
            audioStartElevator.Play();
            activeAudioStart = false;
        }

        while (transform.position != endPoint)
        {
            if (!_pause)
            {
                if (!audioMovementElevator.isPlaying)
                {
                    audioMovementElevator.Play();
                }
                
                activeAudioFinish = true;
                transform.position =
                    Vector3.MoveTowards(transform.position, endPoint, moveSpeed * Time.fixedDeltaTime);
                yield return null;
            }
            else
            {
                if (!audioStopElevator.isPlaying)
                {
                    audioStopElevator.Play();
                }


                yield return new WaitForSeconds(pauseTime);
                _pause = false;
                activeAudioFinish = true;
            }
        }

        if (!audioFinishElevator.isPlaying && activeAudioFinish)
        {
            audioFinishElevator.Play();
            activeAudioFinish = false;
        }

        liftCollider.enabled = false;
        EventManager.Publish(_moveUp ? "OpenTopDoor" : "OpenBottomDoor");
        
        _moveUp = !_moveUp;
        _move = false;
    }
}
