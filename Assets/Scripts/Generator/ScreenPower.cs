using UnityEngine;

namespace Generator
{
    public class ScreenPower : Subscriber
    {
        [SerializeField] private GameObject powerScreen;

        [Event("EnablePowerScreen")]
        private void EnablePowerScreen()
        {
            powerScreen.SetActive(false);
        }

        [Event("DisablePowerScreen")]
        private void DisablePowerScreen()
        {
            powerScreen.SetActive(true);
        }
    }
}