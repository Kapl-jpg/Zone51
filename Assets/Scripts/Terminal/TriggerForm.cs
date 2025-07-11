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
            if (!_canTransform) return;
            if(_wasTransformed) return;
            _wasTransformed = true;
            _canTransform = true;
        }
    }
}