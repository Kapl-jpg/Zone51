using System;
using UnityEngine;

namespace Mechanisms
{
    public class OpenExperimentDoor: Subscriber
    {
        private static readonly int Open = Animator.StringToHash("Open");
        private static readonly int Close = Animator.StringToHash("Close");
        [SerializeField] private Animator doorAnimator;

        [Event("OpenExperimentDoor")]
        private void OpenDoor()
        {
            var getPower = RequestManager.GetValue<bool>("GetPower");
            
            if(getPower)
                doorAnimator.SetTrigger(Open);
        }

        [Event("CloseExperimentDoor")]
        private void CloseDoor()
        {
            doorAnimator.SetTrigger(Close);
        }
    }
}