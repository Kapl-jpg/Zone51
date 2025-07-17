using Enums;
using Interfaces;
using UnityEngine;

public class PlayerInteraction : Subscriber
{
    [SerializeField] private InputManager inputManager;
    private IInteractable _interactable;
    private IFinishable _finishable;
    private bool _showTip;

    private void Update()
    {
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
        if (_interactable != null)
        {
            if (characterType == CharacterType.Human)
            {
                if (!_showTip)
                {
                    EventManager.Publish("EnableTip");
                    EventManager.Publish("ShowTip", TipType.Interact);
                    _showTip = true;
                }

                if (inputManager.InputE())
                {
                    _interactable.Interact();
                }
            }
            else
            {
                _showTip = false;
                EventManager.Publish("HideTip");
            }
        }

        if (_finishable != null)
        {
            if (characterType == CharacterType.Alien)
            {
                if (!_showTip)
                {
                    EventManager.Publish("EnableTip");
                    EventManager.Publish("ShowTip", TipType.Interact);
                    _showTip = true;
                }

                if (inputManager.InputE())
                {
                    _finishable.Finish();
                }
            }
            else
            {
                _showTip = false;
                EventManager.Publish("HideTip");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (other.gameObject.TryGetComponent(out IInteractable interactable))
            {
                _interactable = interactable;
                _interactable.EnableIndicator();
            }

            if (other.gameObject.TryGetComponent(out IFinishable finishable))
            {
                _finishable = finishable;
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
            _showTip = false;
        }
    }

    [Event("DropInteraction")]
    private void DropInteraction()
    {
        _interactable.DisableIndicator();
        EventManager.Publish("HideTip");
        _interactable = null;
    }
}
