using UnityEngine;

public class InputManager : Subscriber
{
    private InputSystem_Actions _input;
    private bool _isLock;

    [Event("PlayerController")]
    private void PlayerController(bool isLock)
    {
        if (isLock)
            _input.Player.Enable();
        else
            _input.Player.Disable();
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

    public bool InputPause()
    {
        return _input.Player.Pause.triggered;
    }
}
