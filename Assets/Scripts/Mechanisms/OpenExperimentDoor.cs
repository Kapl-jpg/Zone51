using System;
using UnityEngine;

namespace Mechanisms
{
    public class OpenExperimentDoor: Subscriber
    {
        private static readonly int Open = Animator.StringToHash("Open");
        private static readonly int Close = Animator.StringToHash("Close");
        [SerializeField] private Animator doorAnimator;
        [SerializeField] private AudioSource audioOpen;
        [SerializeField] private AudioSource audioClose;
        private bool _opened;

        [Event("OpenExperimentDoor")]
        private void OpenDoor()
        {
            if (!_opened) return;
            var getPower = RequestManager.GetValue<bool>("GetPower");

            if (!getPower) return;
            doorAnimator.SetTrigger(Open);
            audioOpen.Play();
            _opened = true;
        }

        [Event("CloseExperimentDoor")]
        private void CloseDoor()
        {
            doorAnimator.SetTrigger(Close);
            audioClose.Play();
        }
    }
}