using System;
using UnityEngine;

namespace Terminal
{
    public class TerminalInput: MonoBehaviour
    {
        private InputSystem_Actions _inputSystem;

        private void Start()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Player.Enable();
        }

        public bool Click()
        {
            return _inputSystem.Player.LeftButtonMouse.triggered;
        }
    }
}