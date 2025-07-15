using Enums;
using Interfaces;
using Terminal;
using Unity.Cinemachine;
using UnityEngine;

public class TerminalInteract : Subscriber, IInteractable
{
    [SerializeField] private CinemachineCamera cinemachine;
    [SerializeField] private new Collider collider;
    [SerializeField] private TerminalInput terminalInput;
    private bool _canInteract = true;
    private bool _interact;
    
    private void Update()
    {
        if(!_interact) return;

        if (RequestManager.GetValue<CharacterType>("CharacterType") == CharacterType.Alien || terminalInput.ExitButton())
        {
            Exit();
        }
    }
    

    public void Interact()
    {
        if(RequestManager.GetValue<bool>("LookTutorial")) return;
        
        cinemachine.Priority = 30;
        collider.enabled = false;
        EventManager.Publish("InterfaceController", true);
        EventManager.Publish("PlayerController", false);
        EventManager.Publish("OnOffCursor", true);
        EventManager.Publish("HideCrosshair");
        EventManager.Publish("HidePlayerVisible");
        EventManager.Publish("HideTip");
        _interact = true;
    }

    public void Exit()
    {
        cinemachine.Priority = 0;
        collider.enabled = _canInteract;
        EventManager.Publish("InterfaceController", false);
        EventManager.Publish("PlayerController", true);
        EventManager.Publish("OnOffCursor", false);
        EventManager.Publish("ShowCrosshair");
        EventManager.Publish("ShowPlayerVisible");
        _interact = false;
    }

    public void DisableTerminal()
    {
        _canInteract = false;
    }
}