using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

public class Tutorials : Subscriber
{
    [SerializeField] private List<TutorialData> tutorials;
    [SerializeField] private TutorialInput input;
    [SerializeField] private float delayTutorial;
    
    private TutorialType _currentTutorial;
    private bool _showTutorial;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delayTutorial);
        EventManager.Publish("ShowTutorial", TutorialType.Movement);
    }

    private void Update()
    {
        if (!input.SkipTutorial()) return;
        
        if(_showTutorial)
            HideTutorial();
    }

    [Event("ShowTutorial")]
    private void ShowTutorial(TutorialType tutorialType)
    {
        _showTutorial = true;
        var data = tutorials.FirstOrDefault(x => x.type == tutorialType);
        Time.timeScale = 0;
        
        foreach (var tutor in data.tutorialUI)
        {
            tutor.gameObject.SetActive(true);
        }
    }
    
    private void HideTutorial()
    {
        _showTutorial = false;
        var data = tutorials.FirstOrDefault(x => x.type == _currentTutorial);
        
        Time.timeScale = 1;
        foreach (var tutor in data.tutorialUI)
        {
            tutor.gameObject.SetActive(false);
        }
    }
}