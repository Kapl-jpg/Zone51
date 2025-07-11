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
    private Rigidbody _rb;
    private bool _hasJumped;
    private AudioSource _whoseJumpEnd;
    private AudioSource _whoseJumpStart;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    [Event("DoJump")]
    private void DoJump()
    {
        if (_hasJumped) return;
        
        _rb.AddForce(Vector3.up * JumpForce(), ForceMode.Impulse);
        _hasJumped = true;
    }

    [Event("ResetJump")]
    private void ResetJump()
    {
        _hasJumped = false;
    }

    private float JumpForce()
    {
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");

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
        _whoseJumpStart?.Play();
    }


    [Event("ActiveAudioJumpEnd")] 
    private void ActiveAudioJumpEnd()
    {
        _whoseJumpEnd?.Play();
    }
}
