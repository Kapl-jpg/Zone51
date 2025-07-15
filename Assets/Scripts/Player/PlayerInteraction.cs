using System;
using Enums;
using Interfaces;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private bool needTransform = true;
    private IInteractable _interactable;

    private void OnTriggerEnter(Collider other)
    {
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
        if (characterType == CharacterType.Alien && needTransform) return;

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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            _interactable.DisableIndicator();
            EventManager.Publish("HideTip");
            
            _interactable = null;
        }
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
        }
    }
}
