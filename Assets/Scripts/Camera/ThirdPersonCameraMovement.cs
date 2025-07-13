using Settings;
using Unity.Cinemachine;
using UnityEngine;

public class ThirdPersonCameraMovement : MonoBehaviour
{
    [SerializeField] private CinemachineOrbitalFollow orbitalFollow;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private MouseManager mouseManager;
    [SerializeField] private Vector2 verticalBorders;

    private void Update()
    {
        orbitalFollow.HorizontalAxis.Value +=
            inputManager.InputMouse().x * mouseManager.MouseSensitivity * Time.deltaTime;
        orbitalFollow.VerticalAxis.Value =
            Mathf.Clamp(
                orbitalFollow.VerticalAxis.Value -
                inputManager.InputMouse().y * mouseManager.MouseSensitivity * Time.deltaTime, verticalBorders.x,
                verticalBorders.y);
    }
}