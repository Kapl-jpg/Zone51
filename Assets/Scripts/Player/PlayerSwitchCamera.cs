using UnityEngine;

namespace Player
{
    public class PlayerSwitchCamera : Subscriber
    {
        [SerializeField] private GameObject thirdPersonCamera;
        [SerializeField] private GameObject firstPersonCamera;

        [Event("FirstPersonCamera")]
        private void OnFirstPersonCamera()
        {
            firstPersonCamera.SetActive(true);
            thirdPersonCamera.SetActive(false);
        }
        
        [Event("ThirdPersonCamera")]
        private void OnThirdPersonCamera()
        {
            thirdPersonCamera.SetActive(true);
            firstPersonCamera.SetActive(false);
        }
        
    }
}