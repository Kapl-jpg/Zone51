using Enums;
using UnityEngine;

public class PlayerMovement : Subscriber
{
    [SerializeField] private float alienSpeedWalking;
    [SerializeField] private float alienSpeedRunning;
    [SerializeField] private float humanSpeedWalking;
    [SerializeField] private float humanSpeedRunning;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float cameraRotationSpeed = 15f;
    [SerializeField] private float movementSmoothing = 0.1f;
    [SerializeField] private float mouseSensitivity = 2f;

    private InputMeneger _inputMeneger;
    private JumpController _jumpController;
    private Rigidbody _rb;
    private Camera _mainCamera;

    private bool _moveSide;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _inputMeneger = GetComponent<InputMeneger>();
        _jumpController = GetComponent<JumpController>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _jumpController.Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveDirection = CheckDirectionMovement()
            ? transform.forward
            : _mainCamera.transform.forward * _inputMeneger.GetMove().y +
              _mainCamera.transform.right * _inputMeneger.GetMove().x;

        CalculationsForMovement(moveDirection);

        _rb.angularVelocity = Vector3.zero;
    }

    private void CalculationsForMovement(Vector3 moveDirection)
    {
        _rb.velocity = new Vector3(moveDirection.x * Speed(), _rb.velocity.y,
            moveDirection.z * Speed());
        //_rb.MovePosition(transform.position + _velocity * Time.fixedDeltaTime);
    }

    [Event("FirstPersonCamera")]
    private void FirstPersonCamera()
    {
        if (_inputMeneger.GetMove().x != 0)
            _moveSide = true;
    }

    private bool CheckDirectionMovement()
    {
        if (!_moveSide) return false;

        if (_inputMeneger.GetMove().x == 0 || _inputMeneger.GetMove().y != 0)
            _moveSide = false;

        return true;
    }

    private float Speed()
    {
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
        if (_inputMeneger.InputShift())
        {
            return characterType == CharacterType.Human ? humanSpeedRunning : alienSpeedRunning;
        }

        return characterType == CharacterType.Human ? humanSpeedWalking : alienSpeedWalking;
    }
}