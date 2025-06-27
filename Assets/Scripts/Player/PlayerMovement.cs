using Enums;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private float alienSpeedWalking;
    [SerializeField] private float alienSpeedRunning;
    [SerializeField] private float humanSpeedWalking;
    [SerializeField] private float humanSpeedRunning;
    [SerializeField] private float rotationSpeed = 10f; // Уменьшено для плавности
    [SerializeField] private float cameraRotationSpeed = 15f; // Добавлен параметр для камеры
    [SerializeField] private float movementSmoothing = 0.1f; // 
    [SerializeField] private float mouseSensitivity = 2f; // 

    //private MouseMovement mouseMovement;
    private InputMeneger inputMeneger;
    private JumpController jumpController;
    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //mouseMovement = GetComponent<MouseMovement>();
        inputMeneger = GetComponent<InputMeneger>();
        jumpController = GetComponent<JumpController>();
    }

    private void Update()
    {
        jumpController.Jump();
    }

    private void FixedUpdate()
    {
        Move();
        //mouseMovement.RotateCharacter();
    }

    private void Move()
    {
        Vector3 moveInput = inputMeneger.GetMove();
        Vector3 cameraForward = virtualCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 moveDirection = cameraForward * moveInput.y + virtualCamera.transform.right * moveInput.x;
        //Vector3 moveDirection = transform.right * inputMeneger.GetMove().x + transform.forward * inputMeneger.GetMove().y;

        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        //velocity = Vector3.SmoothDamp(velocity, moveDirection * Speed(), ref velocity, movementSmoothing);
        rb.linearVelocity = new Vector3(moveDirection.x * Speed(), rb.linearVelocity.y, moveDirection.z * Speed());
        rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        rb.angularVelocity = Vector3.zero;
    }

    private float Speed()
    {
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
        if (inputMeneger.InputShift())
        {
            return characterType == CharacterType.Human ? humanSpeedRunning : alienSpeedRunning;
        }

        return characterType == CharacterType.Human ? humanSpeedWalking : alienSpeedWalking;
    }
}
