using Interfaces;
using UnityEngine;

namespace Lift
{
    public class LiftTrigger: MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            EventManager.Publish("MoveLift");
        }
    }
}