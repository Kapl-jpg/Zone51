using System.Collections;
using Enums;
using UnityEngine;

public class TutorialStart : MonoBehaviour
{
    [SerializeField] private float tutorialDelay;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(tutorialDelay);
        EventManager.Publish("ShowTutorial" , TutorialType.Mouse);
    }
}