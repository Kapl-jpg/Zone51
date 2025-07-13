using UnityEngine;

public class LiftBottomDoor : Subscriber
{
    private static readonly int Open = Animator.StringToHash("Open");
    [SerializeField] private Animator innerDoorAnimator;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioSource audioOpen;
    
    [Event("OpenBottomDoor")]
    private void OpenDoor()
    {
        doorAnimator.SetBool(Open, true);
        innerDoorAnimator.SetBool(Open, true);
        audioOpen.Play();
    }

    [Event("CloseBottomDoor")]
    private void CloseDoor()
    {
        doorAnimator.SetBool(Open, false);
        innerDoorAnimator.SetBool(Open, false);
        audioOpen.Stop();
    }
    
    public void Ready()
    {
        EventManager.Publish("MoveLift");
    }
}