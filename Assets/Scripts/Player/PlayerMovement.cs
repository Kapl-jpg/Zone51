using Enums;
using UnityEngine;

public class PlayerMovement : Subscriber
{
    [SerializeField] private AudioSource audioWalkingUsualAlien;
    [SerializeField] private AudioSource audioWalkingVentilationAlien;
    [SerializeField] private AudioSource audioRunningUsualAlien;
    [SerializeField] private AudioSource audioRunningVentilationAlien;
    [SerializeField] private float alienSpeedWalking;
    [SerializeField] private float alienSpeedRunning;
    [SerializeField] private float humanSpeedWalking;
    [SerializeField] private float humanSpeedRunning;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float cameraRotationSpeed = 15f;
    [SerializeField] private float movementSmoothing = 0.1f;
    [SerializeField] private float mouseSensitivity = 2f;
    
    private InputMeneger _inputMeneger;
    private Rigidbody _rb;
    private Camera _mainCamera;
    private AudioSource whoseWalking;
    private AudioSource whoseRunning;
    private bool _lockMovement;
    private bool _moveSide;
    private bool _isHuman;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _inputMeneger = GetComponent<InputMeneger>();
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Move();
        PlayWalkingOrRunningSound();
    }

    [Event("LockMovement")]
    private void LockMovement(bool lockMovement)
    {
        _lockMovement = lockMovement;
    }

    

    private void Move()
    {
        if (_lockMovement) return;
        
        if(!RequestManager.GetValue<bool>("IsGrounded")) return;
        
        Vector3 moveDirection = _mainCamera.transform.forward * _inputMeneger.GetMove().y + (CheckDirectionMovement()
            ? transform.forward
            : _mainCamera.transform.right) * _inputMeneger.GetMove().x;
        
        CalculationsForMovement(moveDirection);

        _rb.angularVelocity = Vector3.zero;
    }

    private void CalculationsForMovement(Vector3 moveDirection)
    {
        _rb.velocity = new Vector3(moveDirection.x * Speed(), _rb.velocity.y,
            moveDirection.z * Speed());
    }

    [Event("FirstPersonCamera")]
    private void FirstPersonCamera()
    {
        if (_inputMeneger.GetMove().x != 0)
            _moveSide = true;
    }

    private bool CheckDirectionMovement()
    {
        if (_moveSide)
        {
            if (_inputMeneger.GetMove().x == 0 || _inputMeneger.GetMove().y != 0)
                _moveSide = false;
        }

        return _moveSide;
    }

    private float Speed()
    {
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
        if (_inputMeneger.InputShift())
        {
            //return characterType == CharacterType.Human ? humanSpeedRunning : alienSpeedRunning;
            if (characterType == CharacterType.Human)
            {
                _isHuman = true;
                //whoseRunning =
                return humanSpeedRunning;
                
            }
            else
            {
                _isHuman = false;
                whoseRunning = audioRunningUsualAlien;
                return alienSpeedRunning;
                
            }
        }

        //return characterType == CharacterType.Human ? humanSpeedWalking : alienSpeedWalking;

        if (characterType == CharacterType.Human)
        {
            _isHuman = true;
            //whoseWalking =
            return humanSpeedWalking;
        }
        else
        {
            _isHuman = false;
            whoseWalking = audioWalkingUsualAlien;
            return alienSpeedWalking;
        }
        
    }

    private void PlayWalkingOrRunningSound()
    {
        if (!_isHuman)
        {
            if (_inputMeneger.GetMove().magnitude > 0.1f && !_inputMeneger.InputShift() && RequestManager.GetValue<bool>("IsGrounded") && !whoseWalking.isPlaying)
            {
                whoseWalking.Play();
            }
            else if (_inputMeneger.GetMove().magnitude > 0.1f && _inputMeneger.InputShift() && RequestManager.GetValue<bool>("IsGrounded") && !whoseWalking.isPlaying)
            {
                whoseRunning.Play();
            }
        }
        else
        {
            //
        }
        
    }
}