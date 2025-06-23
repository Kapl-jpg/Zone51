using Interfaces;
using UnityEngine;

public class DoorTerminal : MonoBehaviour,  IInteractable
{
    [SerializeField] private DoorAnimation doorAnimation;
    [SerializeField] private GameObject indicator;
    
    public void Interact()
    {
        doorAnimation.OpenDoor();
    }

    public void EnableIndicator()
    {
        indicator.SetActive(true);
    }

    public void DisableIndicator()
    {
        indicator.SetActive(false);
    }
}
