using Enums;
using UnityEngine;

public class JumpController : Subscriber
{
    [SerializeField] private float alienJumpForce;
    [SerializeField] private float humanJumpForce;
    [Header("Ground")] [SerializeField] private float groundCheckRadius;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool showGroundChecker;
    private InputMeneger _inputMeneger;
    private Rigidbody _rb;

    [Request("IsGrounded")] private ObservableField<bool> _isGrounded = new();

    private void Start()
    {
        _inputMeneger = GetComponent<InputMeneger>();
        _rb = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        if (_inputMeneger.Crouch()) return;

        print(CheckGround());
        if (!_inputMeneger.InputSpace() || !CheckGround()) return;
        
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

    private bool CheckGround()
    {
        _isGrounded.Value = Physics.SphereCast(transform.position + Vector3.up, groundCheckRadius, -transform.up, out _,
            groundCheckDistance, groundMask);
        return _isGrounded.Value;
    }

    private void OnDrawGizmosSelected()
    {
        if(!showGroundChecker) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - transform.up * groundCheckDistance, groundCheckRadius);
    }
}
