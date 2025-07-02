using UnityEngine;

namespace Player
{
    public class PlayerRotate : MonoBehaviour
    {
        [SerializeField] private InputMeneger inputMeneger;
        [SerializeField] private float thirdPersonRotationSpeed;
        [SerializeField] private float firstPersonRotationSpeed;

        private float _angle;
        
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
                _angle += inputMeneger.InputMouse().x * firstPersonRotationSpeed;
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