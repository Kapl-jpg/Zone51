using UnityEngine;

namespace Player
{
    public class PlayerRotate : MonoBehaviour
    {
        [SerializeField] private InputMeneger inputMeneger;
        [SerializeField] private float thirdPersonRotationSpeed;
        [SerializeField] private float firstPersonRotationHorizontalSpeed = 10f;
        [SerializeField] private float firstPersonRotationVerticalSpeed = .1f;
        [SerializeField] private Transform targetPoint;
        [SerializeField] private Vector2 minMaxTargetPointOffset = new(-.4f, .6f);
        
        private float _angle;
        private float _targetPointOffset;
        
        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            if (!FirstPersonCamera())
            {
                Vector2 input = inputMeneger.GetMove();
                Vector3 moveDirection = Camera.main.transform.right * input.x + Camera.main.transform.forward * input.y;

                if (!(moveDirection.sqrMagnitude > 0.001f)) return;

                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

                Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

                transform.rotation =
                    Quaternion.RotateTowards(transform.rotation, targetRotation,
                        thirdPersonRotationSpeed * Time.deltaTime);
                _angle = transform.rotation.eulerAngles.y;
            }
            else
            {
                _angle += inputMeneger.InputMouse().x * firstPersonRotationHorizontalSpeed * Time.deltaTime;
                
                _targetPointOffset = Mathf.Clamp(
                    _targetPointOffset + inputMeneger.InputMouse().y * firstPersonRotationVerticalSpeed * Time.deltaTime,
                    minMaxTargetPointOffset.x, 
                    minMaxTargetPointOffset.y);
                
                targetPoint.localPosition = new Vector3(targetPoint.localPosition.x, _targetPointOffset, targetPoint.localPosition.z);
                Quaternion targetRotation = Quaternion.Euler(0, _angle, 0);
                transform.rotation = targetRotation;
            }
        }

        private bool FirstPersonCamera()
        {
            return RequestManager.GetValue<bool>("ActivateFirstPersonCamera");
        }
    }
}