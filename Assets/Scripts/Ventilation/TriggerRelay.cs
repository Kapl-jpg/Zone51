using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerRelay : MonoBehaviour
{ 
    [SerializeField] private UnifiedTriggerZone master;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            master?.NotifyEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            master?.NotifyExit(other);
    }
}