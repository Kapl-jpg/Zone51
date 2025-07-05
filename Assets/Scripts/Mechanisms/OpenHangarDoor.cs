using UnityEngine;

public class OpenHangarDoor : Subscriber
{
    private static readonly int Open = Animator.StringToHash("Open");
    [SerializeField] private Animator doorAnimator;
    
    [Event("OpenHangarDoor")]
    private void OnOpenHangarDoor()
    {
        doorAnimator.SetTrigger(Open);
    }
}