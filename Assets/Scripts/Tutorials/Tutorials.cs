using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

public class Tutorials : Subscriber
{
    [SerializeField] private List<TutorialData> tutorials;
    [SerializeField] private TutorialInput input;
    [SerializeField] private float startDelayTutorial;
    [SerializeField] private float delayTutorial;
    [Request("LookTutorial")]
    private readonly ObservableField<bool> _lookTutorial =  new();
    private TutorialType _currentTutorial;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(startDelayTutorial);
        EventManager.Publish("ShowTutorial", TutorialType.Movement);
    }

    private void Update()
    {
        if (!input.SkipTutorial()) return;
        
        if(_lookTutorial.Value)
            HideTutorial();
    }

    [Event("ShowTutorial")]
    private void ShowTutorial(TutorialType tutorialType)
    {
        StartCoroutine(Show(tutorialType));
    }

    private IEnumerator Show(TutorialType tutorialType)
    {
        if (tutorialType == TutorialType.Telekinesis)
            yield return new WaitForSeconds(delayTutorial);
        
        EventManager.Publish("PlayerController", false);
        _lookTutorial.Value = true;
        _currentTutorial = tutorialType;
        var data = tutorials.FirstOrDefault(x => x.type == tutorialType);
        Time.timeScale = 0;
        
        foreach (var tutor in data.tutorialUI)
        {
            tutor.gameObject.SetActive(true);
        }

        yield return null;
    }
    
    private void HideTutorial()
    {
        EventManager.Publish("PlayerController", true);
        _lookTutorial.Value = false;
        var data = tutorials.FirstOrDefault(x => x.type == _currentTutorial);
        
        Time.timeScale = 1;
        foreach (var tutor in data.tutorialUI)
        {
            tutor.gameObject.SetActive(false);
        }
    }
}