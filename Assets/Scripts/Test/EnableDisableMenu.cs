using System;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class EnableDisableMenu : MonoBehaviour
    {
        [SerializeField] private GraphicRaycaster raycaster;
        private void OnEnable()
        {
            raycaster.enabled = true;
        }

        private void OnDisable()
        {
            raycaster.enabled = false;
        }
    }
}