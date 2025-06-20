using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedWalking;
    [SerializeField] private float speedRunning;

    private MouseMovement mouseMovement;
    private InputMeneger inputMeneger;
    private JumpController jumpController;
    private Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mouseMovement = GetComponent<MouseMovement>();
        inputMeneger = GetComponent<InputMeneger>();
        jumpController = GetComponent<JumpController>();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        mouseMovement.RotateCharacter();
        jumpController.Jump();
    }

    private void MoveCharacter()
    {
        if (inputMeneger.InputShift() == false)
        {
            Move(speedWalking);
        }
        else
        {
            Move(speedRunning);
        }
    }

    private void Move(float speed) 
    {
        Vector3 moveDirection = transform.right * inputMeneger.GetMove().x + transform.forward * inputMeneger.GetMove().y;
        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
    }
}
