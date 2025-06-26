using Enums;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CinemachineCamera freeLookCamera;
    [SerializeField] private float alienSpeedWalking;
    [SerializeField] private float alienSpeedRunning;
    [SerializeField] private float humanSpeedWalking;
    [SerializeField] private float humanSpeedRunning;
    [SerializeField] private float rotationSpeed = 100f;

    //private MouseMovement mouseMovement;
    private InputMeneger inputMeneger;
    private JumpController jumpController;
    private Rigidbody rb;

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
        Vector3 cameraForward = freeLookCamera.transform.forward;
        cameraForward.y = 0; 
        cameraForward.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        
        Vector3 moveDirection = transform.right * inputMeneger.GetMove().x + transform.forward * inputMeneger.GetMove().y;
        rb.linearVelocity = new Vector3(moveDirection.x * Speed(), rb.linearVelocity.y, moveDirection.z * Speed());
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
