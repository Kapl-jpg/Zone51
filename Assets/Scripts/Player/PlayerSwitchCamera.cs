using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Player
{
    public class PlayerSwitchCamera : Subscriber
    {
        [SerializeField] private CinemachineCamera thirdPersonCamera;
        [SerializeField] private CinemachineCamera firstPersonCamera;
        
        [SerializeField] private CinemachineInputAxisController inputAxisController;
        [SerializeField] private CinemachineOrbitalFollow orbitalFollow;
        
        [SerializeField] private Transform player;
        
        [SerializeField] private int maxPriority;
        [SerializeField] private int minPriority;
        
        private bool _firstPerson;
        
        [Event("FirstPersonCamera")]
        private void OnFirstPersonCamera()
        {
            firstPersonCamera.Priority = maxPriority;
            thirdPersonCamera.Priority = minPriority;
            inputAxisController.enabled = false;
            _firstPerson = true;
        }
        
        [Event("ThirdPersonCamera")]
        private void OnThirdPersonCamera()
        {
            firstPersonCamera.Priority = minPriority;
            thirdPersonCamera.Priority = maxPriority;
            inputAxisController.enabled = true;
            _firstPerson = false;
        }

        private void Update()
        {
            if(_firstPerson)
                orbitalFollow.HorizontalAxis.Value = player.localEulerAngles.y;
        }
    }
}