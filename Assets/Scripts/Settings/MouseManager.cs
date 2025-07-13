using UnityEngine;

namespace Settings
{
    [CreateAssetMenu (menuName = "Settings/MouseManager" , fileName = "MouseManager")]
    public class MouseManager : ScriptableObject
    {
        private float _mouseSensitivity;
        public float MouseSensitivity => _mouseSensitivity;

        public void SetMouseSensitivity(float sensitivity)
        {
            _mouseSensitivity = sensitivity;
        }
    }
}