using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMeneger : Subscriber
{
    private InputSystem_Actions _input;
    private bool _isLock;

    [Event("LockController")]
    private void LockController(bool isLock)
    {
        if (isLock)
        {
            _input.Player.Disable();
            _input.UI.Enable();
        }
        else
        {
            _input.Player.Enable();
            _input.UI.Disable();
        }
    }

    private void Start()
    {
        _input = new InputSystem_Actions();
        _input.Player.Enable();
    }

    public bool InputShift()
    {
        return _input.Player.Sprint.IsPressed();
    }

    public Vector2 InputMouse()
    {
        return _input.Player.Look.ReadValue<Vector2>();
    }

    public Vector2 GetMove()
    {
        return _input.Player.Move.ReadValue<Vector2>();
    }

    public bool InputSpace()
    {
        return _input.Player.Jump.triggered;
    }

    public bool InputE()
    {
        return _input.Player.Interact.triggered;
    }

    public bool InputMouseLeftButton()
    {
        return _input.Player.LeftButtonMouse.triggered;
    }

    public bool InputMouseRightButton()
    {
        return _input.Player.RightButtonMouse.IsPressed();
    }

    public bool Transformation()
    {
        return _input.Player.Transformation.triggered;
    }

    public bool Crouch()
    {
        return _input.Player.Crouch.IsPressed();
    }
}
