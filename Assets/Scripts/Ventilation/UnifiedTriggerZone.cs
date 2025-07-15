using System.Collections.Generic;
using UnityEngine;

public class UnifiedTriggerZone : Subscriber
{
    [SerializeField] private bool isVentilation = true;
    [Request("ActivateFirstPersonCamera")] 
    private ObservableField<bool> _isActiveFirstPersonCamera = new();

    private readonly Dictionary<Collider, int> _colliderCounts = new();

    public void NotifyEnter(Collider other)
    {
        if (!_colliderCounts.TryGetValue(other, out int count))
        {
            _colliderCounts[other] = 1;

            if (_colliderCounts.Count == 1)
            {
                EventManager.Publish("FirstPersonCamera");
                EventManager.Publish("Ventilation", true);
                if(isVentilation)
                    EventManager.Publish("ActiveAudioInVentilation", false);
                _isActiveFirstPersonCamera.Value = true;
            }
        }
        else
        {
            _colliderCounts[other] = count + 1;
        }
    }

    public void NotifyExit(Collider other)
    {
        if (!_colliderCounts.TryGetValue(other, out int count)) return;

        if (count <= 1)
        {
            _colliderCounts.Remove(other);

            if (_colliderCounts.Count == 0)
            {
                EventManager.Publish("ThirdPersonCamera");
                EventManager.Publish("Ventilation", false);
                if(isVentilation)
                    EventManager.Publish("ActiveAudioInVentilation", true);
                _isActiveFirstPersonCamera.Value = false;
            }
        }
        else
        {
            _colliderCounts[other] = count - 1;
        }
    }
}