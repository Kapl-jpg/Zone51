using Enums;
using Interfaces;
using Unity.Cinemachine;
using UnityEngine;

public class TerminalInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private CinemachineCamera cinemachine;
    [SerializeField] private Collider collider;
    [SerializeField] private bool canUseOneTime;
    [SerializeField] private bool needClosed;
    private bool _interact;
    
    private void Update()
    {
        if(!_interact) return;
        
        if (RequestManager.GetValue<CharacterType>("CharacterType") == CharacterType.Alien)
        {
            Exit();
        }
    }

    public void Interact()
    {
        cinemachine.Priority = 30;
        collider.enabled = false;
        EventManager.Publish("InterfaceController", true);
        EventManager.Publish("PlayerController", false);
        EventManager.Publish("OnOffCursor", true);
        EventManager.Publish("HideInterface");
        EventManager.Publish("HidePlayerVisible");
        _interact = true;
    }

    public void Exit()
    {
        cinemachine.Priority = 0;
        print(canUseOneTime);
        if(!canUseOneTime)
            collider.enabled = true;
        EventManager.Publish("InterfaceController", false);
        EventManager.Publish("PlayerController", true);
        EventManager.Publish("OnOffCursor", false);
        EventManager.Publish("ShowInterface");
        EventManager.Publish("ShowPlayerVisible");
        _interact = false;
    }
}