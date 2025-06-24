using System.Collections;
using Enums;
using UnityEngine;

namespace Player
{
    public class PlayerState : Subscriber
    {
        [SerializeField] private InputMeneger input;
        [SerializeField] private float humanDuration;
        
        private float _humanDurationTimer;

        private bool _transformation;
        
        [Request("CharacterType")] 
        private readonly ObservableField<CharacterType> _characterType = new(CharacterType.Alien);
        
        private void Update()
        {
            if (!input.Transformation()) return;
            
            if (_transformation) return;
            
            print(_characterType.Value);
            if (_characterType.Value == CharacterType.Alien)
            {
                StartCoroutine(StayHuman());
            }
            
            if(_characterType.Value == CharacterType.Human)
            {
                StartCoroutine(StayAlien());
            }
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

        private IEnumerator StayHuman()
        {
            _humanDurationTimer = humanDuration;
            
            EventManager.Publish("SwitchForm", CharacterType.Human);

            while (_humanDurationTimer > 0f)
            {
                _humanDurationTimer -= Time.deltaTime;
                yield return null;
            }
            
            EventManager.Publish("SwitchForm", CharacterType.Alien);
        }

        private IEnumerator StayAlien()
        {
            StopCoroutine(StayHuman());
            _humanDurationTimer = 0f;
            
            EventManager.Publish("SwitchForm", CharacterType.Alien);
            yield return null;
        }
    }
}