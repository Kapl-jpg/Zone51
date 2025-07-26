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
        terminalInput.EnableInput();
        EventManager.Publish("DisableTip");
        EventManager.Publish("InterfaceController", true);
        EventManager.Publish("PlayerController", false);
        EventManager.Publish("OnOffCursor", true);
        EventManager.Publish("HideCrosshair");
        EventManager.Publish("HidePlayerVisible");
        _interact = true;
    }

    public void Exit()
    {
        cinemachine.Priority = 0;
        collider.enabled = _canInteract;
        terminalInput.DisableInput();
        EventManager.Publish("EnableTip");
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