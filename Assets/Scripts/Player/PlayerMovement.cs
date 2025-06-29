using Enums;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CinemachineCamera thirdPersonCamera;
    [SerializeField] private float alienSpeedWalking;
    [SerializeField] private float alienSpeedRunning;
    [SerializeField] private float humanSpeedWalking;
    [SerializeField] private float humanSpeedRunning;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float cameraRotationSpeed = 15f;
    [SerializeField] private float movementSmoothing = 0.1f;
    [SerializeField] private float mouseSensitivity = 2f;

    private MouseMovement mouseMovement;
    private InputMeneger inputMeneger;
    private JumpController jumpController;
    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mouseMovement = GetComponent<MouseMovement>();
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

        if (ActiveFirstPersonCamera())
        {
            mouseMovement.RotateCharacter();
        }
    }

    private void Move()
    {
        Vector3 moveInput = inputMeneger.GetMove();
        Vector3 cameraForward = thirdPersonCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        if (!ActiveFirstPersonCamera())
        {
            Vector3 moveDirection = cameraForward * moveInput.y + thirdPersonCamera.transform.right * moveInput.x;

            if (moveDirection.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }

            CalculationsForMovement(moveDirection);
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            Vector3 moveDirection = transform.right * inputMeneger.GetMove().x + transform.forward * inputMeneger.GetMove().y;
            CalculationsForMovement(moveDirection);
        }        
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

    private bool ActiveFirstPersonCamera()
    {
        return RequestManager.GetValue<bool>("ActivateFirstPersonCamera");
    }

    private void CalculationsForMovement(Vector3 moveDirection)
    {
        rb.velocity = new Vector3(moveDirection.x * Speed(), rb.velocity.y, moveDirection.z * Speed());
        rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }
}
