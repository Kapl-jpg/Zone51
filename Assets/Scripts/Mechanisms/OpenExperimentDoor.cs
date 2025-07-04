using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Mechanisms
{
    public class OpenExperimentDoor: Subscriber
    {
        private static readonly int Open = Animator.StringToHash("Open");
        [SerializeField] private Animator doorAnimator;
    
        [Event("OpenExperimentDoor")]
        private void OnOpenMainDoor()
        {
            var getPower = RequestManager.GetValue<bool>("GetPower");
            if(getPower)
                doorAnimator.SetTrigger(Open);
        }
    }
}