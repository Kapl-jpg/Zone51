using UnityEngine;

namespace Player
{
    public class CursorController : Subscriber
    {
        private void Start()
        {
            OnOffCursor(false);
        }

        [Event("OnOffCursor")]
        private void OnOffCursor(bool active)
        {
            if (active == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}