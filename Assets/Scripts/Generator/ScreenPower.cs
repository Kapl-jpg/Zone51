using UnityEngine;
using UnityEngine.UI;

namespace Generator
{
    public class ScreenPower : Subscriber
    {
        [SerializeField] private GameObject powerScreen;
        [SerializeField] private Collider interactionCollider;
        [SerializeField] private Button openDoorButton;

        [Event("EnablePowerScreen")]
        private void EnablePowerScreen()
        {
            openDoorButton.interactable = true;
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