using Interfaces;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private InputMeneger inputMeneger;
    [SerializeField] public float sphereCastRadius = 1.5f; 
    [SerializeField] public float maxDistance = 5f;
    
    private IInteractable _interactable;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, sphereCastRadius, transform.forward, out hit, maxDistance))
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
                _interactable.Interact();
        }
    }
}
