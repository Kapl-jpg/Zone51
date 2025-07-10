using UnityEngine;

namespace Player
{
    public class InterfaceController: Subscriber
    {
        [SerializeField] private GameObject crosshair;
        
        [Event("ShowCrosshair")]
        private void ShowCrosshair()
        {
            crosshair.SetActive(true);
        }

        [Event("HideCrosshair")]
        private void HideCrosshair()
        {
            crosshair.SetActive(false);
        }
    }
}