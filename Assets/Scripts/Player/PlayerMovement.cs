using Enums;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
    private readonly Vector3 _velocity = Vector3.zero;
    private Camera _mainCamera;

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
        Vector3 moveDirection = _mainCamera.transform.forward * _inputMeneger.GetMove().y +
                                _mainCamera.transform.right * _inputMeneger.GetMove().x;

        CalculationsForMovement(moveDirection);

        _rb.angularVelocity = Vector3.zero;
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

    private void CalculationsForMovement(Vector3 moveDirection)
    {
        _rb.velocity = new Vector3(moveDirection.x * Speed(), _rb.velocity.y, moveDirection.z * Speed());
        _rb.MovePosition(transform.position + _velocity * Time.fixedDeltaTime);
    }
}
