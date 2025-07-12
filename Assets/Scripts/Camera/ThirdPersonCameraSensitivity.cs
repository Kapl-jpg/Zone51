using Settings;
using Unity.Cinemachine;
using UnityEngine;

public class ThirdPersonCameraSensitivity : MonoBehaviour
{
    [SerializeField] private CinemachineOrbitalFollow orbitalFollow;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private MouseManager mouseManager;

    private void Update()
    {
        orbitalFollow.HorizontalAxis.Value += inputManager.InputMouse().x * mouseManager.MouseSensitivity * Time.deltaTime;
        orbitalFollow.VerticalAxis.Value -= inputManager.InputMouse().y * mouseManager.MouseSensitivity * Time.deltaTime;
    }
}