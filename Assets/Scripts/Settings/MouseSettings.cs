using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class MouseSettings: MonoBehaviour
    {
        [SerializeField] private MouseManager mouseManager;
        [SerializeField] private Slider mouseSlider;
        
        [SerializeField] private float minSensitivity;
        [SerializeField] private float maxSensitivity;

        private void Start()
        {
            mouseSlider.value = Mathf.InverseLerp(minSensitivity, maxSensitivity, mouseManager.MouseSensitivity);
        }

        public void ChangeSensitivity()
        {
            mouseManager.SetMouseSensitivity(Mathf.Lerp(minSensitivity, maxSensitivity, mouseSlider.value));
        }
    }
}