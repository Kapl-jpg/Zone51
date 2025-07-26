using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Generator
{
    public class ScreenPower : Subscriber
    {
        [SerializeField] private GameObject powerScreen;
        [SerializeField] private Collider interactionCollider;
        [SerializeField] private Button[] interactionButtons;
        [SerializeField] private GameObject noPowerPanel;
        [SerializeField] private GameObject videoPanel;
        [SerializeField] private VideoPlayer videoPlayer;

        public void CheckPower()
        {
            if (RequestManager.GetValue<bool>("GetPower"))
            {
                videoPanel.SetActive(true);
                videoPlayer.Play();
            }
            else
            {
                noPowerPanel.SetActive(true);
                foreach (var button in interactionButtons)
                {
                    button.enabled = false;
                }
            }
        }
            
        [Event("EnablePowerScreen")]
        private void EnablePowerScreen()
        {
            powerScreen.SetActive(false);
            interactionCollider.enabled = true;
        }

        [Event("DisablePowerScreen")]
        private void DisablePowerScreen()
        {
            powerScreen.SetActive(true);
            interactionCollider.enabled = false;
        }
    }
}