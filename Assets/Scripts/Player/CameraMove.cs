using Enums;
using UnityEngine;

namespace Player
{
    public class CameraMove : Subscriber
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform alienTarget;
        [SerializeField] private Transform humanTarget;

        [Event("ChangeForm")]
        private void ChangeCamera()
        {
            var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
            
            if(characterType == CharacterType.Human)
                cameraTransform.localPosition = humanTarget.localPosition;
            if(characterType == CharacterType.Alien)
                cameraTransform.localPosition = alienTarget.localPosition;
        }
    }
}