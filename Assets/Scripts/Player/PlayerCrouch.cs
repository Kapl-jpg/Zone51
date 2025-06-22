using Enums;
using UnityEngine;

namespace Player
{
    public class PlayerCrouch : Subscriber
    {
        [SerializeField] private GameObject crouchBody;
        [SerializeField] private InputMeneger input;
        
        private bool _isCrouching;

        private void Update()
        {
            if (input.Crouch())
            {
                var characterType = RequestManager.GetValue<CharacterType>("CharacterType");

                if (characterType == CharacterType.Human) return;

                if (_isCrouching) return;
                
                var scale = crouchBody.transform.localScale;
                scale = new Vector3(scale.x, scale.y/2, scale.z);
                crouchBody.transform.localScale = scale;
                
                var position = crouchBody.transform.position;
                position.y = transform.position.y - scale.y / 4;
                crouchBody.transform.position = position;
                
                _isCrouching = true;
            }
            else
            {
                if (!_isCrouching) return;
                
                var scale = crouchBody.transform.localScale;
                scale = new Vector3(scale.x, scale.y * 2, scale.z);
                crouchBody.transform.localScale = scale;
                
                var position = crouchBody.transform.position;
                position.y = transform.position.y + scale.y / 4;
                crouchBody.transform.position = position;
                
                _isCrouching = false;
            }
        }
    }
}