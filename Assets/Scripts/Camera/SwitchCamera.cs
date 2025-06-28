using Unity.Cinemachine;
using UnityEngine;

public class SwitchCamera : Subscriber
{
    [SerializeField] CinemachineCamera thirdPersonCamera;
    [SerializeField] CinemachineCamera firstPersonCamera;

    private int _priorityThirdPersonCamera;
    private int _priorityFirstPersonCamera;

    [Request("ActivateFirstPersonCamera")] private ObservableField<bool> _isActiveFirstPersonCamera = new();

    private void Start()
    {
        _priorityThirdPersonCamera = thirdPersonCamera.Priority;
        _priorityFirstPersonCamera = firstPersonCamera.Priority;
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.name == "Player") 
        {
            
            if (thirdPersonCamera.Priority > firstPersonCamera.Priority)
            {
                firstPersonCamera.Priority = _priorityThirdPersonCamera;
                thirdPersonCamera.Priority = _priorityFirstPersonCamera;
                _isActiveFirstPersonCamera.Value = true;
            }
            else
            {
                thirdPersonCamera.Priority = _priorityThirdPersonCamera;
                firstPersonCamera.Priority = _priorityFirstPersonCamera;
                _isActiveFirstPersonCamera.Value = false;
            }
        }
    }
}
