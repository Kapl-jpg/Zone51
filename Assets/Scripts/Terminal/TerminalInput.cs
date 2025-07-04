using UnityEngine;

namespace Terminal
{
    public class TerminalInput : Subscriber
    {
        private InputSystem_Actions _inputSystem;

        private void Start()
        {
            _inputSystem = new InputSystem_Actions();
        }

        [Event("LockController")]
        private void EnableLockController(bool enabled)
        {
            if (enabled)
            {
                Debug.LogError("EnableLockController");
                _inputSystem.UI.Enable();
            }
            else
            {
                Debug.LogError("DisableLockController");
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