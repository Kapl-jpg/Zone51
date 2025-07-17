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
    [SerializeField] private float moveTime;
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
        if (!_move)
            CloseDoor();
    }

    [Event("Landing")]
    private void ResetJump()
    {
        EventManager.Publish("ActiveAudioJumpEnd");
        _pause = true;
    }

    [Event("MoveLift")]
    private void Movement()
    {
        //print("Move");
        Debug.LogError("LiftMovement: Movement()");
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
        var startPoint = _moveUp ? downPoint.position : upPoint.position;
        var endPoint = _moveUp ? upPoint.position : downPoint.position;
        if (!audioStartElevator.isPlaying && activeAudioStart)
        {
            audioStartElevator.Play();
            activeAudioStart = false;
        }
        
        EventManager.Publish("DisableMusic");

        var t = 0f;
        while (t < 1f)
        {
            if (!_pause)
            {
                if (!audioMovementElevator.isPlaying)
                {
                    audioMovementElevator.Play();
                }

                t += Time.deltaTime / moveTime;
                activeAudioFinish = true;
                transform.position =
                    Vector3.Lerp(startPoint, endPoint, t);
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

        if(_moveUp)
            EventManager.Publish("EnableGameMusic");
        else
            EventManager.Publish("EnableHangarMusic");
        
        EventManager.Publish("EnableMusic");

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
