using System;
using UnityEngine;

namespace Classic
{
    public class ResetGround: MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            EventManager.Publish("ResetPlate");
        }
    }
}