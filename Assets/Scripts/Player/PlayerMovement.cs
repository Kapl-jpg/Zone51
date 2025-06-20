using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedWalking;
    [SerializeField] private float speedRunning;

    private MouseMovement mouseMovement;
    private InputMeneger inputMeneger;
    private Rigidbody rb;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mouseMovement = GetComponent<MouseMovement>();
        inputMeneger = GetComponent<InputMeneger>();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        mouseMovement.RotateCharacter();
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
        Vector3 moveDirection = transform.TransformDirection(new Vector3(inputMeneger.GetMove().x, 0, inputMeneger.GetMove().y));
        rb.AddForce(moveDirection * speed);
    }
}
