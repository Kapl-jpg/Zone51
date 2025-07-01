using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerRelay : MonoBehaviour
{ 
    [SerializeField] private UnifiedTriggerZone master;

    private void OnTriggerEnter(Collider other)
    {
        master?.NotifyEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        master?.NotifyExit(other);
    }
}