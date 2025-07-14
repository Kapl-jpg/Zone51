using Enums;
using Interfaces;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] public float sphereCastRadius = 1.5f; 
    [SerializeField] public float maxDistance = 5f;
    [SerializeField] private bool needTransform = true;
    private IInteractable _interactable;

    private void Update()
    {
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
        if (characterType == CharacterType.Alien && needTransform) return;

        if (Physics.SphereCast(transform.position - UnityEngine.Camera.main.transform.forward, sphereCastRadius,
                UnityEngine.Camera.main.transform.forward, out var hit, maxDistance))
        {
            hit.collider.TryGetComponent(out IInteractable interactable);
            if (interactable != null)
            {
                if (_interactable == null)
                {
                    _interactable = interactable;
                    EventManager.Publish("ShowTip", TipType.Interact);
                    _interactable.EnableIndicator();
                }
            }
        }
        else
        {
            if (_interactable != null)
            {
                EventManager.Publish("HideTip");
                _interactable.DisableIndicator();
                _interactable = null;
            }
        }
        
        if (inputManager.InputE())
        {
            if (_interactable != null)
            {
                _interactable.Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        
        Gizmos.DrawWireSphere(UnityEngine.Camera.main.transform.position + UnityEngine.Camera.main.transform.forward * maxDistance, sphereCastRadius);
    }
}
