using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private InputMeneger inputMeneger;
    [SerializeField] private Animator alienAnimator;
    [SerializeField] private Animator humanAnimator;
    [SerializeField] private float translateTime;
    
    private static readonly int IsGrounded = Animator.StringToHash("Ground");
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int Crouch = Animator.StringToHash("Crouch");
    private static readonly int Jump = Animator.StringToHash("Jump");

    private float _horizontal;
    private float _vertical;
    
    private void Update()
    {
        SetGrounded();
        SetMovement();
        SetCrouching();
        SetJump();
    }

    [Event("ResetJump")]
    private void ResetJump()
    {
        if(alienAnimator.gameObject.activeInHierarchy)
            alienAnimator.ResetTrigger(Jump);
        if(humanAnimator.gameObject.activeInHierarchy)
            humanAnimator?.ResetTrigger(Jump);
    }

    private void SetJump()
    {
        if (!Grounded()) return;
        if (inputMeneger.Crouch()) return;
        if (!inputMeneger.InputSpace()) return;
        if (RequestManager.GetValue<bool>("IsCrouching")) return;

        if (alienAnimator.gameObject.activeInHierarchy)
            alienAnimator?.SetTrigger(Jump);
        if (humanAnimator.gameObject.activeInHierarchy)
            humanAnimator?.SetTrigger(Jump);
    }

    private void SetGrounded()
    {
        if(alienAnimator.gameObject.activeInHierarchy)
            alienAnimator?.SetBool(IsGrounded, Grounded());
        if(humanAnimator.gameObject.activeInHierarchy)
            humanAnimator?.SetBool(IsGrounded, Grounded());
    }

    private void SetMovement()
    {
        if (alienAnimator.gameObject.activeInHierarchy)
        {
            alienAnimator?.SetFloat(MoveX, Move().x);
            alienAnimator?.SetFloat(MoveY, Move().y);
        }

        if (humanAnimator.gameObject.activeInHierarchy)
        {
            humanAnimator?.SetFloat(MoveX, Move().x);
            humanAnimator?.SetFloat(MoveY, Move().y);
        }
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
        if(alienAnimator.gameObject.activeInHierarchy)
            alienAnimator?.SetBool(Crouch, inputMeneger.Crouch());
    }

    private bool Grounded()
    {
        return RequestManager.GetValue<bool>("IsGrounded");
    }
}
