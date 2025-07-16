using UnityEngine;

namespace Terminal
{
    public class TerminalInput : Subscriber
    {
        private InputSystem_Actions _inputSystem;

        private void Start()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.UI.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _inputSystem.UI.Disable();
        }

        public bool Click()
        {
            return _inputSystem.UI.Click.triggered;
        }

        public Vector2 MousePosition()
        {
            return _inputSystem.UI.Point.ReadValue<Vector2>();
        }

        public bool ExitButton()
        {
            return _inputSystem.UI.Exit.triggered; 
        }
    }
}