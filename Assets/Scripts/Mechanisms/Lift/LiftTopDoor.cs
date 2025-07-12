using UnityEngine;

public class LiftTopDoor : Subscriber
{
    private static readonly int Open = Animator.StringToHash("Open");
    [SerializeField] private Animator innerDoorAnimator;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioSource audioOpen;
    
    [Event("OpenTopDoor")]
    private void OpenDoor()
    {
        innerDoorAnimator.SetBool(Open, true);
        doorAnimator.SetBool(Open, true);
        audioOpen.Play();
    }

    [Event("CloseTopDoor")]
    private void CloseDoor()
    {
        innerDoorAnimator.SetBool(Open, false);
        doorAnimator.SetBool(Open, false);
        audioOpen.Stop();
    }

    public void Ready()
    {
        EventManager.Publish("MoveLift");
    }
}