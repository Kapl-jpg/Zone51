using System;
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
            var sensitivity = Mathf.Lerp(minSensitivity, maxSensitivity, mouseSlider.value);
            mouseManager.SetMouseSensitivity(sensitivity);
        }
    }
}