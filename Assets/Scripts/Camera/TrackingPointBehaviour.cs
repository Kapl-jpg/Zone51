using UnityEngine;

namespace Camera
{
    public class TrackingPointBehaviour : Subscriber
    {
        [SerializeField] private Transform thirdPersonTarget;
        [SerializeField] private Transform firstPersonTarget;
        [SerializeField] private Vector2 minMaxTargetPointOffset = new(-.4f, .6f);
        [SerializeField] private float firstPersonRotationVerticalSpeed = .1f;
        [SerializeField] private InputMeneger inputMeneger;
        [SerializeField] private new UnityEngine.Camera camera;
        private readonly float _currentSensitivity = 1f;
        private float _targetPointOffset;

        [Event("Ventilation")]
        private void ChangeCamera(bool ventilation)
        {
            EventManager.Publish(ventilation ? "HidePlayerVisible" : "ShowPlayerVisible");
        }

        private void Update()
        {
            if (RequestManager.GetValue<bool>("ActivateFirstPersonCamera"))
            {
                _targetPointOffset = Mathf.Clamp(
                    _targetPointOffset + inputMeneger.InputMouse().y * firstPersonRotationVerticalSpeed *
                    _currentSensitivity * Time.deltaTime,
                    minMaxTargetPointOffset.x,
                    minMaxTargetPointOffset.y);

                firstPersonTarget.localPosition =
                    new Vector3(firstPersonTarget.localPosition.x, _targetPointOffset,
                        firstPersonTarget.localPosition.z);
            }
        }
    }
}