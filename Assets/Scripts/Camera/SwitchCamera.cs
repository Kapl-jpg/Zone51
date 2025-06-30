using UnityEngine;

public class SwitchCamera : Subscriber
{

    [Request("ActivateFirstPersonCamera")] 
    private ObservableField<bool> _isActiveFirstPersonCamera = new();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        EventManager.Publish("FirstPersonCamera");
        
        _isActiveFirstPersonCamera.Value = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        EventManager.Publish("ThirdPersonCamera");
        
        _isActiveFirstPersonCamera.Value = false;
    }
}
