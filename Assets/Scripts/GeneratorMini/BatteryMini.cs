using System.Collections;
using Interfaces;
using UnityEngine;

namespace GeneratorMini
{
    public class BatteryMini : MonoBehaviour, IInteractable
    {
        [SerializeField] private ParticleSystem particles;
        private bool _used;
        
        public void Interact()
        {
            if(_used) return;

            StartCoroutine(DisableParticles());
            EventManager.Publish("RemoveBattery");
            _used = true;
        }

        private IEnumerator DisableParticles()
        {
            particles.Stop();
            while (particles.isEmitting)
            {
                yield return null;
            }
            particles.gameObject.SetActive(false);
        }
    }
}