using Enums;
using UnityEngine;

namespace Player
{
    public class PlayerCrouch : Subscriber
    {
        [SerializeField] private CapsuleCollider alienCollider;
        [SerializeField] private InputManager input;
        [Request("IsCrouching")] private readonly ObservableField<bool> _isCrouching =  new();
        private Vector3 _defaultCenter;
        private float _defaultHeight;
        
        private Vector3 _crouchCenter;
        private float _crouchHeight;
        
        private bool _ventilationEnabled;

        private void Start()
        {
            _defaultCenter = alienCollider.center;
            _defaultHeight = alienCollider.height;
            
            _crouchCenter = alienCollider.center - Vector3.up * 0.25f;
            _crouchHeight = alienCollider.height * 0.7f;
        }

        private void Update()
        {
            Crouch();
        }

        [Event("Ventilation")]
        private void Ventilation(bool value)
        {
            _ventilationEnabled = value;
            _isCrouching.Value = value;
            if (!value) return;
            
            alienCollider.center = _crouchCenter;
            alienCollider.height = _crouchHeight;
            EventManager.Publish("Crouch", true);
        }

        private void Crouch()
        {
            if(_ventilationEnabled) return;
            
            var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
            if (characterType == CharacterType.Human) return;

            _isCrouching.Value = input.Crouch();
            if (input.Crouch())
            {
                alienCollider.center = _crouchCenter;
                alienCollider.height = _crouchHeight;
                EventManager.Publish("Crouch", true);
            }
            else
            {
                alienCollider.center = _defaultCenter;
                alienCollider.height = _defaultHeight;
                EventManager.Publish("Crouch", false);
            }
        }
    }
}