using Interfaces;
using Unity.Cinemachine;
using UnityEngine;

public class TerminalInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private CinemachineCamera cinemachine;
    [SerializeField] private Collider collider;
    
    public void Interact()
    {
        cinemachine.Priority = 30;
        collider.enabled = false;
        EventManager.Publish("LockController", true);
        EventManager.Publish("OnOffCursor", true);
        EventManager.Publish("HideInterface");
    }

    public void Exit()
    {
        cinemachine.Priority = 0;
        collider.enabled = true;
        EventManager.Publish("LockController", false);
        EventManager.Publish("OnOffCursor", false);
        EventManager.Publish("ShowInterface");
    }
}