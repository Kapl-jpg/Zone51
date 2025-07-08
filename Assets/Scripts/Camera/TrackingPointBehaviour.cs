using UnityEngine;

namespace Camera
{
    public class TrackingPointBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform thirdPersonTarget;
        [SerializeField] private Transform firstPersonTarget;
        [SerializeField] private Vector2 minMaxTargetPointOffset = new(-.4f, .6f);
        [SerializeField] private float firstPersonRotationVerticalSpeed = .1f;
        [SerializeField] private InputMeneger inputMeneger;
        [SerializeField] private new UnityEngine.Camera camera;
        private readonly float _currentSensitivity = 1f;
        private float _targetPointOffset;

        private void Update()
        {
            if (FirstPersonCamera())
            {
                if(LayerEnabled())
                    EventManager.Publish("ShowPlayerVisible");
                _targetPointOffset = Mathf.Clamp(
                    _targetPointOffset + inputMeneger.InputMouse().y * firstPersonRotationVerticalSpeed *
                    _currentSensitivity * Time.deltaTime,
                    minMaxTargetPointOffset.x,
                    minMaxTargetPointOffset.y);

                firstPersonTarget.localPosition =
                    new Vector3(firstPersonTarget.localPosition.x, _targetPointOffset,
                        firstPersonTarget.localPosition.z);
            }
            else
            {
                if(!LayerEnabled())
                    EventManager.Publish("HidePlayerVisible");
            }
        }

        private bool LayerEnabled()
        {
            return RequestManager.GetValue<bool>("PlayerIsVisible");
        }

        private bool FirstPersonCamera()
        {
            return RequestManager.GetValue<bool>("ActivateFirstPersonCamera");
        }
    }
}