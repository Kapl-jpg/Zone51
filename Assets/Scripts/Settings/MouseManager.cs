using UnityEngine;

namespace Settings
{
    [CreateAssetMenu (menuName = "Settings/MouseManager" , fileName = "MouseManager")]
    public class MouseManager : ScriptableObject
    {
        private float _mouseSensitivity = 4f;
        public float MouseSensitivity => _mouseSensitivity;

        public void SetMouseSensitivity(float sensitivity)
        {
            _mouseSensitivity = sensitivity;
            ES3.Save("MouseSensitivity", sensitivity);
        }
    }
}