using Settings;
using UnityEngine;

namespace Player
{
    public class PlayerRotate : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private MouseManager mouseManager;
        [SerializeField] private float thirdPersonRotationSpeed;
        [SerializeField] private float firstPersonRotationHorizontalSpeed = 10f;
        
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
                Vector3 moveDirection = UnityEngine.Camera.main.transform.forward;

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
                _angle += inputManager.InputMouse().x * firstPersonRotationHorizontalSpeed * mouseManager.MouseSensitivity * Time.deltaTime;
                
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