using System;
using Enums;
using UnityEngine;

public class JumpController : Subscriber
{
    [SerializeField] private AudioSource audioJumpStartAlien;
    [SerializeField] private AudioSource audioJumpEndAlien;
    [SerializeField] private float alienJumpForce;
    [SerializeField] private float humanJumpForce;
    [SerializeField] private bool showGroundChecker;
    private InputMeneger _inputMeneger;
    private Rigidbody _rb;

    private AudioSource _whoseJumpEnd;
    private AudioSource _whoseJumpStart;

    private void Start()
    {
        _inputMeneger = GetComponent<InputMeneger>();
        _rb = GetComponent<Rigidbody>();
    }

    [Event("DoJump")]
    private void DoJump()
    {
        Vector2 input = _inputMeneger.GetMove();
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = Camera.main.transform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 direction = right * input.x + forward * input.y;
        Vector3 jumpDirection = (direction + Vector3.up).normalized;

        _rb.AddForce(jumpDirection * JumpForce(), ForceMode.Impulse);
    }

    private float JumpForce()
    {
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
        //return characterType == CharacterType.Human ? humanJumpForce : alienJumpForce;

        if (characterType == CharacterType.Human)
        {
            _whoseJumpStart = null;
            _whoseJumpEnd = null;
            return humanJumpForce;
        }
        else
        {
            _whoseJumpStart = audioJumpStartAlien;
            _whoseJumpEnd = audioJumpEndAlien;
            return alienJumpForce;
        }
    }

    [Event("ActiveAudioJumpStart")]
    private void ActiveAudioJumpStart()
    {
        _whoseJumpStart.Play();
    }


    [Event("ActiveAudioJumpEnd")] 
    private void ActiveAudioJumpEnd()
    {
        _whoseJumpEnd.Play();
    }
}
