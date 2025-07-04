using System.Collections;
using Enums;
using UnityEngine;

namespace Player
{
    public class PlayerState : Subscriber
    {
        [SerializeField] private InputMeneger input;
        [SerializeField] private float humanDuration;
        [SerializeField] private bool needGetAgility = true;
        private float _humanDurationTimer;

        private bool _transformation;
        private IEnumerator _humanCoroutine;
        
        [Request("CharacterType")] 
        private readonly ObservableField<CharacterType> _characterType = new(CharacterType.Alien);
        
        [Request("ChipDisable")]
        private readonly ObservableField<bool> _chipDisable = new();
        
        private void Update()
        {
            if(!_chipDisable.Value && needGetAgility) return;
            
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

        [Event("ForcedTransformation")]
        private void ForcedTransformation(CharacterType characterType)
        {
            if (_characterType.Value == characterType) return;
            if (characterType == CharacterType.Alien)
            {
                StartCoroutine(StayAlien());
                _chipDisable.Value = true;
            }
            
            if(characterType == CharacterType.Human)
            {
                StartCoroutine(StayHuman(true));
            }
            _characterType.Value = characterType;
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