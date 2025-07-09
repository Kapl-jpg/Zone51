using UnityEngine;

namespace Player
{
    public class InterfaceController: Subscriber
    {
        [SerializeField] private GameObject crosshair;
        
        [Event("ShowInterface")]
        private void ShowInterface()
        {
            crosshair.SetActive(true);
        }

        [Event("HideInterface")]
        private void HideInterface()
        {
            crosshair.SetActive(false);
        }
    }
}