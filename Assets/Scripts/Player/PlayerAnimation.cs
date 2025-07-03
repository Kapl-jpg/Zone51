using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private InputMeneger inputMeneger;
    [SerializeField] private Animator animator;
    [SerializeField] private float translateTime;
    
    private static readonly int IsGrounded = Animator.StringToHash("Ground");
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int Crouch = Animator.StringToHash("Crouch");

    private float _horizontal;
    private float _vertical;
    
    private void Update()
    {
        SetGrounded();
        SetMovement();
        SetCrouching();
    }

    private void SetGrounded()
    {
        var isGrounded = RequestManager.GetValue<bool>("IsGrounded");
        animator.SetBool(IsGrounded, isGrounded);
    }

    private void SetMovement()
    {
        animator.SetFloat(MoveX, Move().x);
        animator.SetFloat(MoveY, Move().y);
    }

    private Vector2 Move()
    {
        if (inputMeneger.GetMove().x > 0)
            _horizontal = Mathf.MoveTowards(_horizontal,1,Time.deltaTime / translateTime);
        else if (inputMeneger.GetMove().x < 0)
            _horizontal = Mathf.MoveTowards(_horizontal,-1,Time.deltaTime / translateTime);
        else
            _horizontal = Mathf.MoveTowards(_horizontal,0,Time.deltaTime / translateTime);
        
        if (inputMeneger.GetMove().y > 0)
            _vertical = Mathf.MoveTowards(_vertical,1,Time.deltaTime / translateTime);
        else if (inputMeneger.GetMove().y < 0)
            _vertical = Mathf.MoveTowards(_vertical,-1,Time.deltaTime / translateTime);
        else
            _vertical = Mathf.MoveTowards(_vertical,0,Time.deltaTime / translateTime);
        
        if (inputMeneger.InputShift())
        {
            return new Vector2(_horizontal,_vertical) * 2;
        }

        return new Vector2(_horizontal,_vertical);
    }

    private void SetCrouching()
    {
        animator.SetBool(Crouch, true);
    }
}
