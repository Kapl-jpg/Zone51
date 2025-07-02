using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorsForTerminal : Subscriber
{
    private static readonly int Open = Animator.StringToHash("Open");
    [SerializeField] private Animator doorAnimator;

    [Event("OpenDoorForTerminal")]
    private void OpenDoorForTerminal()
    {
        doorAnimator.SetTrigger(Open);
    }

}
