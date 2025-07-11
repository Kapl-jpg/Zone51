using System.Collections;
using Enums;
using UnityEngine;

namespace Player
{
    public class PlayerState : Subscriber
    {
        [SerializeField] private InputManager input;
        [SerializeField] private float humanDuration;
        [SerializeField] private bool needGetAbility = true;
        [SerializeField] private bool tutorial;
        private float _humanDurationTimer;
        private bool _ventilationEnabled;

        private bool _transformation;
        private IEnumerator _humanCoroutine;
        
        [Request("CharacterType")] 
        private readonly ObservableField<CharacterType> _characterType = new(CharacterType.Alien);
        
        [Request("ChipDisable")]
        private readonly ObservableField<bool> _chipDisable = new();
        
        private void Update()
        {
            if(_ventilationEnabled) return;
            if(!_chipDisable.Value && needGetAbility) return;
            if (!input.Transformation()) return;
            if (_transformation) return;
            
            if (_characterType.Value == CharacterType.Alien)
            {
                StartCoroutine(StayHuman(false));
            }
            
            if(_characterType.Value == CharacterType.Human)
            {
                StartCoroutine(StayAlien());
            }
        }

        [Event("Ventilation")]
        private void Ventilation(bool value)
        {
            _ventilationEnabled = value;
        }

        [Event("ForcedTransformation")]
        private void ForcedTransformation()
        {
                StartCoroutine(StayHuman(true));
            
            _chipDisable.Value = true;
            _characterType.Value = CharacterType.Human;
        }

        [Event("Transformation")]
        private void Transformation(bool transformation)
        {
            _transformation = transformation;
        }

        [Event("SetForm")]
        private void SetForm(CharacterType characterType)
        {
            _characterType.Value = characterType;
        }

        private IEnumerator StayHuman(bool endless)
        {
            _humanDurationTimer = humanDuration;
            
            EventManager.Publish("SwitchForm", CharacterType.Human);

            if (!tutorial)
            {
                if (!endless)
                {
                    while (_humanDurationTimer > 0f)
                    {
                        _humanDurationTimer -= Time.deltaTime;
                        yield return null;
                    }

                    EventManager.Publish("SwitchForm", CharacterType.Alien);
                }
            }
        }

        private IEnumerator StayAlien()
        {
            StopCoroutine(StayHuman(true));
            StopCoroutine(StayHuman(false));
            
            _humanDurationTimer = 0f;
            
            EventManager.Publish("SwitchForm", CharacterType.Alien);
            yield return null;
        }
    }
}