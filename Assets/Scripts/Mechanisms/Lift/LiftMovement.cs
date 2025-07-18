using System.Collections;
using UnityEngine;

public class LiftMovement : Subscriber
{
    [SerializeField] private Transform upPoint;
    [SerializeField] private Transform downPoint;
    [SerializeField] private AudioSource liftMusic;
    [SerializeField] private AudioSource audioMovementElevator;
    [SerializeField] private AudioSource audioStartElevator;
    [SerializeField] private AudioSource audioFinishElevator;
    [SerializeField] private AudioSource audioStopElevator; // ???
    [SerializeField] private float moveTime;
    [SerializeField] private float pauseTime;
    [SerializeField] private Collider liftCollider;
    [SerializeField] private Material liftUpperButton;
    [SerializeField] private Material liftBottomButton;
    [SerializeField] private Material liftArrowsPanel;
    private Transform _player;
    private bool _move;
    private bool _liftMovement;
    private bool _moveUp;
    private bool _pause;
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
        
        _liftMovement = true;
        
        if (_moveUp)
        {
            liftArrowsPanel.SetFloat("_Up", 1f);
            liftUpperButton.SetFloat("_Enable", 1f);
        }
        else
        {
            liftArrowsPanel.SetFloat("_Up", 0f);
            liftBottomButton.SetFloat("_Enable", 1f);
        }
        liftArrowsPanel.SetFloat("_Enable", 1f);
        if (!audioStartElevator.isPlaying && activeAudioStart)
        {
            audioStartElevator.Play();
            activeAudioStart = false;
        }
        print(audioFinishElevator.isPlaying);
        
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

                if (!_liftMovement)
                {
                    if (_moveUp)
                    {
                        liftUpperButton.SetFloat("_Enable", 1f);
                    }
                    else
                    {
                        liftBottomButton.SetFloat("_Enable", 1f);
                    }
                    liftArrowsPanel.SetFloat("_Enable", 1f);
                }

                if (!liftMusic.isPlaying)
                {
                    liftMusic.Play();
                }

                t += Time.deltaTime / moveTime;
                transform.position =
                    Vector3.Lerp(startPoint, endPoint, t);
                yield return null;
            }
            else
            {
                EventManager.Publish("DisableLift");
                if (!audioStopElevator.isPlaying)
                {
                    audioStopElevator.Play();
                }
                if (audioMovementElevator.isPlaying)
                {
                    audioMovementElevator.Pause();
                }
                if (liftMusic.isPlaying)
                {
                    liftMusic.Pause();
                }
                
                if (_liftMovement)
                {
                    if (_moveUp)
                        liftUpperButton.SetFloat("_Enable", 0f);
                    else
                        liftBottomButton.SetFloat("_Enable", 0f);
                    liftArrowsPanel.SetFloat("_Enable", 0f);
                }
                
                _liftMovement = false;
                yield return new WaitForSeconds(pauseTime);
                
                EventManager.Publish("EnableLift");
                _pause = false;
            }
        }

        if (_moveUp)
        {
            liftUpperButton.SetFloat("_Enable", 0f);
            EventManager.Publish("EnableGameMusic");
        }
        else
        {
            liftBottomButton.SetFloat("_Enable", 0f);
            EventManager.Publish("EnableHangarMusic");
        }

        liftArrowsPanel.SetFloat("_Enable", 0f);
        EventManager.Publish("EnableMusic");
        
        audioFinishElevator.Play();

        liftCollider.enabled = false;
        
        EventManager.Publish(_moveUp ? "OpenTopDoor" : "OpenBottomDoor");
        
        _moveUp = !_moveUp;
        _move = false;
    }

    private void OnApplicationQuit()
    {
        liftArrowsPanel.SetFloat("_Enable", 0f);
        liftArrowsPanel.SetFloat("_Up", 0f);
        liftUpperButton.SetFloat("_Enable", 0f);
        liftBottomButton.SetFloat("_Enable", 0f);
    }
}
