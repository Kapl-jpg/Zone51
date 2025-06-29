using UnityEngine;

public class OpenDoor : Subscriber
{
    private static readonly int Open = Animator.StringToHash("Open");
    [SerializeField] private Animator doorAnimator;
    
    [Event("OpenMainDoor")]
    private void OpenMainDoor()
    {
        doorAnimator.SetTrigger(Open);
    }
}