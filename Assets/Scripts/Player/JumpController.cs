using Enums;
using UnityEngine;

public class JumpController : Subscriber
{
    [SerializeField] private float alienJumpForce;
    [SerializeField] private float humanJumpForce;

    private InputMeneger _inputMeneger;
    private Rigidbody _rb;

    [Request("IsGrounded")]
    private readonly ObservableField<bool> _isGrounded =  new();

    private void Start()
    {
        _inputMeneger = GetComponent<InputMeneger>();
        _rb = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        if (_inputMeneger.Crouch()) return;

        if (!_inputMeneger.InputSpace() || !_isGrounded.Value) return;
        
        _isGrounded.Value = false;
        DoJump();
    }

    [Event("DoJump")]
    public void DoJump()
    {
        _rb.AddForce(Vector3.up * JumpForce(), ForceMode.Impulse);
    }

    private float JumpForce()
    {
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
        
        return characterType == CharacterType.Human ? humanJumpForce : alienJumpForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ForJump"))
        {
            _isGrounded.Value = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ForJump"))
        {
            _isGrounded.Value = false;
        }
    }
}
