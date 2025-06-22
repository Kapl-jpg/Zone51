using System.Collections;
using Enums;
using UnityEngine;

namespace Player
{
    public class PlayerState : Subscriber
    {
        [SerializeField] private InputMeneger input;
        
        [SerializeField] private float humanDuration;
        [SerializeField] private float humanCooldown;
        
        private float _humanDurationTimer;
        private float _humanCooldownTimer;
        
        [Request("CharacterType")] 
        private readonly ObservableField<CharacterType> _characterType = new(CharacterType.Alien);
        
        private void Update()
        {
            if (!CanTransform()) return;
            
            if (input.Transformation())
            {
                StartCoroutine(ChangeForm());
            }
        }

        private IEnumerator ChangeForm()
        {
            _humanDurationTimer = humanDuration;
            _humanCooldownTimer = humanCooldown;
            
            _characterType.Value = CharacterType.Human;
            EventManager.Publish("ChangeForm");

            while (_humanDurationTimer > 0f)
            {
                _humanDurationTimer -= Time.deltaTime;
                yield return null;
            }
            
            _characterType.Value = CharacterType.Alien;
            EventManager.Publish("ChangeForm");
            StartCoroutine(RestoreCooldown());
        }

        private IEnumerator RestoreCooldown()
        {
            while (_humanCooldownTimer > 0f)
            {
                _humanCooldownTimer -= Time.deltaTime;
                yield return null;
            }
        }
        
        private bool CanTransform()
        {
            return _humanDurationTimer <= 0 && _humanCooldownTimer <= 0;;
        }
    }
}