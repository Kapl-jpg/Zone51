using Unity.Cinemachine;
using UnityEngine;

public class AdjustCameraToModel : Subscriber
{
    [SerializeField] private CinemachineOrbitalFollow thirdPersonCamera;
    [SerializeField] private float radiusCameraForAlien;
    [SerializeField] private float radiusCameraForHuman;
    [SerializeField] private float radiusForSittingAlien;

    [Event("CameraForAlien")]
    private void CameraForAlien()
    {
        thirdPersonCamera.Radius = radiusCameraForAlien;
    }

    [Event("CameraForHuman")]
    private void CameraForHuman()
    {
        thirdPersonCamera.Radius = radiusCameraForHuman;
    }

    [Event("CameraForSittingAlien")]
    private void CameraForSittingAlien()
    {
        thirdPersonCamera.Radius = radiusForSittingAlien;
    }
}
