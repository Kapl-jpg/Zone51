using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Terminal
{
    public class TerminalInput : Subscriber
    {
        private InputSystem_Actions _inputSystem;

        private void Start()
        {
            _inputSystem = new InputSystem_Actions();
        }

        [Event("InterfaceController")]
        private void InterfaceController(bool enabled)
        {
            if (enabled)
            {
                _inputSystem.UI.Enable();
            }
            else
            {
                _inputSystem.UI.Disable();
            }
        }

        public bool Click()
        {
            return _inputSystem.UI.Click.triggered;
        }

        public Vector2 MousePosition()
        {
            return _inputSystem.UI.Point.ReadValue<Vector2>();
        }
    }
}