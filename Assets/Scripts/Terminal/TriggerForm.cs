using System.Collections;
using Enums;
using UnityEngine;

namespace Terminal
{
    public class TriggerForm: MonoBehaviour
    {
        [SerializeField] private float cooldown;
        private bool _canTransform;
        private bool _wasTransformed;
        
        public void CanTransform(bool can)
        {
            if(_wasTransformed) return;
            _canTransform = can;
        }

        public void ChangeForm()
        {
            if (_canTransform)
            {
                StartCoroutine(Change());
                print("Change");
                _wasTransformed = true;
            }
        }

        private IEnumerator Change()
        {
            yield return new WaitForSeconds(cooldown);
            EventManager.Publish("ForcedTransformation", CharacterType.Alien);
        }
    }
}