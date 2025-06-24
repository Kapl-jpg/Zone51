using Enums;
using UnityEngine;

namespace Player
{
    public class CameraTransformation : Subscriber
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform alienStayTarget;
        [SerializeField] private Transform alienCrouchTarget;
        [SerializeField] private Transform humanTarget;

        [Event("SwitchForm")]
        private void ChangeCamera(CharacterType characterType)
        {
            if(characterType == CharacterType.Human)
                cameraTransform.position = humanTarget.position;
            if(characterType == CharacterType.Alien)
                cameraTransform.position = alienStayTarget.position;
        }

        [Event("Crouch")]
        private void Crouch(bool crouch)
        {
            if (crouch)
            {
                cameraTransform.position = alienCrouchTarget.position;
                return;
            }
            
            cameraTransform.position = alienStayTarget.position;
        }
    }
}