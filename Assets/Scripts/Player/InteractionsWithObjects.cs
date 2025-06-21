using UnityEngine;

public class InteractionsWithObjects : MonoBehaviour
{
    [SerializeField] public GameObject interactionButton;
    [SerializeField] public float sphereCastRadius = 1.5f; 
    [SerializeField] public float maxDistance = 5f;                                            
    [SerializeField] private string interactableTag = "Interactable";

    private void Start()
    {
        //interactionButton.SetActive(false);
    }

    private void Update()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, sphereCastRadius, transform.forward, out hit, maxDistance))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                interactionButton.SetActive(true);
            }
            else
            {
                interactionButton.SetActive(false);
            }
        }
        else
        {
            interactionButton.SetActive(false);
        }
    }
}
