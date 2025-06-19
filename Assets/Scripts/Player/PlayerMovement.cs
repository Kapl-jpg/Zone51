using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedWalking;
    [SerializeField] private float speedRunning;

    private MouseMovement mouseMovement;
    private InputSystem_Actions input;
    private Rigidbody rb;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mouseMovement = GetComponent<MouseMovement>();
        input = new InputSystem_Actions();
        input.Player.Enable();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        mouseMovement.RotateCharacter();
    }

    public Vector2 GetMove()
    {
        return input.Player.Move.ReadValue<Vector2>();
    }

    public bool InputShift()
    {
        return input.Player.Sprint.IsPressed();
    }

    private void MoveCharacter()
    {
        if (InputShift() == false)
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
        Vector3 moveDirection = transform.TransformDirection(new Vector3(GetMove().x, 0, GetMove().y));
        rb.AddForce(moveDirection * speed);
    }
}
