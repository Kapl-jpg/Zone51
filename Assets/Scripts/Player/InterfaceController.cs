using UnityEngine;

namespace Player
{
    public class InterfaceController: Subscriber
    {
        [Event("ShowInterface")]
        private void ShowInterface()
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(true);
        }

        [Event("HideInterface")]
        private void HideInterface()
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);
        }
    }
}