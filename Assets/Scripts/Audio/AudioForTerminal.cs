using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioForTerminal : MonoBehaviour
{
    [SerializeField] private AudioSource audioClickForTerminal;
    [SerializeField] private AudioSource audioGuidanceOnButtons;

    public void ClickForTerminal()
    {
        audioClickForTerminal.Play();
    }

    public void GuidanceOnButtons()
    {
        audioGuidanceOnButtons.Play();
    }
}
