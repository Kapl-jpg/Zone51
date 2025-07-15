using Enums;
using Interfaces;
using UnityEngine;

public class PlayerInteraction : Subscriber
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private bool needTransform = true;
    private IInteractable _interactable;
    private IFinishable _finishable;

    private void OnTriggerEnter(Collider other)
    {
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
        if (characterType != CharacterType.Alien || !needTransform)
        {
            if (other.CompareTag("Interactable"))
            {
                if (other.gameObject.TryGetComponent(out IInteractable interactable))
                {
                    _interactable = interactable;
                    _interactable.EnableIndicator();
                    EventManager.Publish("ShowTip", TipType.Interact);
                }
            }
        }
        
        if(characterType == CharacterType.Alien)
        {
            if (other.CompareTag("Interactable"))
            {
                if (other.gameObject.TryGetComponent(out IFinishable finishable))
                {
                    _finishable = finishable;
                    EventManager.Publish("ShowTip", TipType.Interact);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if(_interactable != null)
                _interactable.DisableIndicator();
            EventManager.Publish("HideTip");
            _finishable = null;
            _interactable = null;
        }
    }

    [Event("DropInteraction")]
    private void DropInteraction()
    {
        _interactable.DisableIndicator();
        EventManager.Publish("HideTip");
        _interactable = null;
    }

    private void Update()
    {
        if (inputManager.InputE())
        {
            if (_interactable != null)
            {
                EventManager.Publish("HideTip");
                _interactable.Interact();
            }

            if (_finishable != null)
            {
                EventManager.Publish("HideTip");
                _finishable.Finish();
            }
        }
    }
}
