using UnityEngine;

namespace Generator
{
    public class ScreenPower : Subscriber
    {
        [SerializeField] private GameObject powerScreen;
        [SerializeField] private Collider interactionCollider;

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