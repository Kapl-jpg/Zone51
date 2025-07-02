using UnityEngine;

public class OpenMainDoor : Subscriber
{
    private static readonly int Open = Animator.StringToHash("Open");
    [SerializeField] private Animator doorAnimator;
    
    [Event("OpenMainDoor")]
    private void OnOpenMainDoor()
    {
        doorAnimator.SetTrigger(Open);
    }
}