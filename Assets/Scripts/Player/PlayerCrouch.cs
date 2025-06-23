using Enums;
using UnityEngine;

namespace Player
{
    public class PlayerCrouch : Subscriber
    {
        [SerializeField] private GameObject crouchBody;
        [SerializeField] private InputMeneger input;

        private Vector3 _scale;
        private bool _isCrouching;

        private void Start()
        {
            _scale = crouchBody.transform.localScale;
        }

        private void Update()
        {
            if (input.Crouch())
            {
                var characterType = RequestManager.GetValue<CharacterType>("CharacterType");

                if (characterType == CharacterType.Human) return;

                if (_isCrouching) return;
                
                var scale = crouchBody.transform.localScale;
                var position = crouchBody.transform.position;
                
                crouchBody.transform.localScale = new Vector3(_scale.x, _scale.y/2, _scale.z);
                //crouchBody.transform.position = new Vector3(transform.position.x, transform.position.y - _scale.y / 4, transform.position.z);
                
                _isCrouching = true;
                EventManager.Publish("Crouch", true);
            }
            else
            {
                if (!_isCrouching) return;
                
                
                crouchBody.transform.localScale = new Vector3(_scale.x, _scale.y, _scale.z);
                //crouchBody.transform.position = new Vector3(transform.position.x, transform.position.y + _scale.y / 4, transform.position.z);

                _isCrouching = false;
                EventManager.Publish("Crouch", false);
            }
        }
    }
}