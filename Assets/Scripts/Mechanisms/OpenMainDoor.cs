using UnityEngine;

public class OpenMainDoor : Subscriber
{
    private static readonly int Open = Animator.StringToHash("Open");
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioSource audiOpen;
    
    [Event("OpenMainDoor")]
    private void OnOpenMainDoor()
    {
        doorAnimator.SetTrigger(Open);
        audiOpen.Play();
    }
}