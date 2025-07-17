using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

public class Tutorials : Subscriber
{
    [SerializeField] private List<TutorialData> tutorials;
    [SerializeField] private GameObject closeTip;
    [SerializeField] private TutorialInput input;
    private TutorialType _currentTutorial;

    private void Update()
    {
        if (!input.SkipTutorial()) return;
            HideTutorial();
    }

    [Event("ShowTutorial")]
    private void ShowTutorial(TutorialType tutorialType)
    {
        EventManager.Publish("PlayerController", false);
        _currentTutorial = tutorialType;
        closeTip.SetActive(true);
        
        var data = tutorials.FirstOrDefault(x => x.type == tutorialType);
        data?.tutorialUI.SetActive(true);

        Time.timeScale = 0;
    }
    
    private void HideTutorial()
    {
        EventManager.Publish("PlayerController", true);
        closeTip.SetActive(false);
        
        var data = tutorials.FirstOrDefault(x => x.type == _currentTutorial);
        data?.tutorialUI.SetActive(false);

        if (_currentTutorial == TutorialType.Mouse)
        {
            ShowTutorial(TutorialType.Movement);
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}