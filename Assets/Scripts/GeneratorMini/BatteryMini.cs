using System.Collections;
using Interfaces;
using UnityEngine;

namespace GeneratorMini
{
    public class BatteryMini : MonoBehaviour, IInteractable
    {
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private GeneratorMiniBehaviour miniBehaviour;
        private bool _used;
        
        public void Interact()
        {
            if(_used) return;

            StartCoroutine(DisableParticles());
            miniBehaviour.RemoveBattery();
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