using System;
using Enums;
using UnityEngine;

public class PlayerMovement : Subscriber
{
    [SerializeField] private AudioSource audioWalkingUsualAlien;
    [SerializeField] private AudioSource audioWalkingVentilationAlien;
    [SerializeField] private AudioSource audioRunningUsualAlien;
    [SerializeField] private AudioSource audioRunningVentilationAlien;
    [SerializeField] private AudioSource audioWalkingUsualHuman;
    [SerializeField] private AudioSource audioRunningUsualHuman;
    [SerializeField] private float alienSpeedWalking;
    [SerializeField] private float alienSpeedRunning;
    [SerializeField] private float humanSpeedWalking;
    [SerializeField] private float humanSpeedRunning;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float cameraRotationSpeed = 15f;
    [SerializeField] private float movementSmoothing = 0.1f;
    [SerializeField] private float mouseSensitivity = 2f;
    
    private InputManager _inputManager;
    private Rigidbody _rb;
    private UnityEngine.Camera _mainCamera;
    private AudioSource whoseWalking;
    private AudioSource whoseRunning;
    private bool _moveSide;
    private bool _isHuman;
    private bool _activeAudioByGender;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _inputManager = GetComponent<InputManager>();
        _mainCamera = UnityEngine.Camera.main;
        _activeAudioByGender = true;
    }

    private void FixedUpdate()
    {
        Move();
        PlayWalkingOrRunningSound(); //Commit
    }

    private void Move()
    {
        Vector3 moveDirection = _mainCamera.transform.forward * _inputManager.GetMove().y + (CheckDirectionMovement()
            ? transform.forward
            : _mainCamera.transform.right) * _inputManager.GetMove().x;
        
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
        if (_inputManager.GetMove().x != 0)
            _moveSide = true;
    }

    private bool CheckDirectionMovement()
    {
        if (_moveSide)
        {
            if (_inputManager.GetMove().x == 0 || _inputManager.GetMove().y != 0)
                _moveSide = false;
        }

        return _moveSide;
    }

    private float Speed()
    {
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
        if (_inputManager.InputShift())
        {
            //return characterType == CharacterType.Human ? humanSpeedRunning : alienSpeedRunning;
            if (characterType == CharacterType.Human)
            {
                _isHuman = true;
                whoseRunning = audioRunningUsualHuman;
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
            whoseWalking = audioWalkingUsualHuman;
            return humanSpeedWalking;
        }
        else
        {
            _isHuman = false;
            whoseWalking = audioWalkingUsualAlien;
            return alienSpeedWalking;
        }
        
    }

    [Event("PlayWalkingOrRunningSound")]
    private void PlayWalkingOrRunningSound()
    {
        if (!_isHuman && _activeAudioByGender)
        {

            if (_inputManager.GetMove().magnitude > 0.1f && !_inputManager.InputShift() && RequestManager.GetValue<bool>("IsGrounded") && !whoseWalking.isPlaying)
            {
                whoseWalking.Play();
            }
            else if (_inputManager.GetMove().magnitude > 0.1f && _inputManager.InputShift() && RequestManager.GetValue<bool>("IsGrounded") && !whoseRunning.isPlaying)
            {
                whoseRunning.Play();
            }
            
        }
        else if (!_isHuman && !_activeAudioByGender)
        {
            if (_inputManager.GetMove().magnitude > 0.1f && _inputManager.InputShift() && !audioRunningVentilationAlien.isPlaying)
            {
                audioRunningVentilationAlien.Play();
            }
            else if (_inputManager.GetMove().magnitude > 0.1f && !_inputManager.InputShift() && !audioWalkingVentilationAlien.isPlaying)
            {
                audioWalkingVentilationAlien.Play(); 
            }
        }
        
    }

    [Event("ActiveAudioInVentilation")]

    private void ActiveAudioInVentilation(bool active)
    {
        _activeAudioByGender = active;
    }
}