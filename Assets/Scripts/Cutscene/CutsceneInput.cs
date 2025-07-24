using System;
using TMPro;
using UnityEngine;

public class CutsceneInput : MonoBehaviour
{
    [SerializeField] private float holdTimer;
    [SerializeField] private TMP_Text skipText;

    private InputSystem_Actions _inputSystem;
    private float _skipTimer;

    private void Start()
    {
        _inputSystem = new InputSystem_Actions();
        _inputSystem.UI.Enable();
    }

    private void OnDestroy()
    {
        _inputSystem?.UI.Disable();
    }

    private void Update()
    {
        if(_inputSystem.UI.Click.triggered)
            EventManager.Publish("NextSlide");
    }
}
