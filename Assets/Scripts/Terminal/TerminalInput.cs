using System;
using UnityEngine;

namespace Terminal
{
    public class TerminalInput : MonoBehaviour
    {
        private InputSystem_Actions _inputSystem;

        private void Start()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.UI.Enable();
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