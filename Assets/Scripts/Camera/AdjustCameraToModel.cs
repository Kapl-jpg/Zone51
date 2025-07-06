using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class AdjustCameraToModel : Subscriber
{
    [SerializeField] private CinemachineOrbitalFollow thirdPersonCamera;
    [SerializeField] private float radiusCameraForAlien;
    [SerializeField] private float radiusCameraForHuman;
    [SerializeField] private float radiusForSittingAlien;
    [SerializeField] private float timeChangeRadius;

    [Event("CameraForAlien")]
    private void CameraForAlien()
    {
        StartCoroutine(ChangeRange(radiusCameraForAlien));
    }

    [Event("CameraForHuman")]
    private void CameraForHuman()
    {
        StartCoroutine(ChangeRange(radiusCameraForHuman));
    }

    [Event("CameraForSittingAlien")]
    private void CameraForSittingAlien()
    {
        StartCoroutine(ChangeRange(radiusForSittingAlien));
    }

    private IEnumerator ChangeRange(float targetRange)
    {
        var radius = thirdPersonCamera.Radius;
        var t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / timeChangeRadius;
            thirdPersonCamera.Radius = Mathf.Lerp(radius, targetRange, t);
            yield return null;
        }
    }
}
