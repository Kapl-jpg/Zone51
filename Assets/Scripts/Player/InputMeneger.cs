using UnityEngine;

public class InputMeneger : MonoBehaviour
{
    private InputSystem_Actions input;

    private void Start()
    {
        input = new InputSystem_Actions();
        input.Player.Enable();
    }

    public bool InputShift()
    {
        return input.Player.Sprint.IsPressed();
    }

    public Vector2 InputMouse()
    {
        return input.Player.Look.ReadValue<Vector2>();
    }

    public Vector2 GetMove()
    {
        return input.Player.Move.ReadValue<Vector2>();
    }

    public bool InputSpace()
    {
        return input.Player.Jump.triggered;
    }

    public bool InputE()
    {
        return input.Player.Interact.triggered;
    }

    public bool InputMouseLeftButton()
    {
        return input.Player.LeftButtonMouse.triggered;
    }

    public bool InputMouseRightButton()
    {
        return input.Player.RightButtonMouse.IsPressed();
    }
}
