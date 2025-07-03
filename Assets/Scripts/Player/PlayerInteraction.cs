using Enums;
using Interfaces;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private InputMeneger inputMeneger;
    [SerializeField] public float sphereCastRadius = 1.5f; 
    [SerializeField] public float maxDistance = 5f;
    [SerializeField] private bool needTransform = true;
    private IInteractable _interactable;

    private void Update()
    {
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
        if (characterType == CharacterType.Alien && needTransform) return;
        
        if (Physics.SphereCast(Camera.main.transform.position, sphereCastRadius, Camera.main.transform.forward, out var hit, maxDistance))
        {
            hit.collider.TryGetComponent(out IInteractable interactable);
            if (interactable != null)
            {
                if (_interactable == null)
                {
                    _interactable = interactable;
                    _interactable.EnableIndicator();
                }
            }
        }
        else
        {
            if (_interactable != null)
            {
                _interactable.DisableIndicator();
                _interactable = null;
            }
        }
        
        if (inputMeneger.InputE())
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
        
        Gizmos.DrawWireSphere(Camera.main.transform.position + Camera.main.transform.forward * maxDistance, sphereCastRadius);
    }
}
