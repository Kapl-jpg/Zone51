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
            RequestManager.SetValue("ChipDisable", true);
            EventManager.Publish("ShowTutorial", TutorialType.Transformation);
            _wasTransformed = true;
            _canTransform = true;
        }
    }
}