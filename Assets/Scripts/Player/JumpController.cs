using Enums;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField] private float alienJumpForce;
    [SerializeField] private float humanJumpForce;

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
        if (inputMeneger.Crouch()) return;
        
        if (inputMeneger.InputSpace() && activeJump)
        {
            rb.AddForce(Vector3.up * JumpForce(), ForceMode.Impulse);

            activeJump = false;
        }
    }

    private float JumpForce()
    {
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
        
        return characterType == CharacterType.Human ? humanJumpForce : alienJumpForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ForJump")
        {
            activeJump = true;
        }
    }
}
