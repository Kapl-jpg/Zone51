using System;
using UnityEngine;

namespace Mechanisms
{
    public class OpenExperimentDoor : Subscriber
    {
        private static readonly int Open = Animator.StringToHash("Open");
        private static readonly int Close = Animator.StringToHash("Close");
        [SerializeField] private Animator doorAnimator;
        [SerializeField] private AudioSource audioOpen;
        [SerializeField] private AudioSource audioClose;
        private bool _isOpen = false;

        [Event("OpenExperimentDoor")]
        private void OpenDoor()
        {
            if (_isOpen) return; // Если дверь уже открыта, ничего не делаем

            var getPower = RequestManager.GetValue<bool>("GetPower");
            if (!getPower) return; // Если нет питания, не открываем

            doorAnimator.SetTrigger(Open);
            audioOpen.Play();
            _isOpen = true;
        }

        [Event("CloseExperimentDoor")]
        private void CloseDoor()
        {
            if (!_isOpen) return; // Если дверь уже закрыта, ничего не делаем

            doorAnimator.SetTrigger(Close);
            audioClose.Play();
            _isOpen = false;
        }

    }
}