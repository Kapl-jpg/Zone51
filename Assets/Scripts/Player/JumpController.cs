using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField] private float jumpForce;

    private InputMeneger inputMeneger;
    private Rigidbody rb;

    private bool activeJump;

    private void Start()
    {
        inputMeneger = GetComponent<InputMeneger>();
        rb = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        if (inputMeneger.InputSpace() && activeJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            activeJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ForJump")
        {
            activeJump = true;
        }
    }
}
