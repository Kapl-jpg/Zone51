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

    private void Update()
    {
        var holdLbm = _inputSystem.UI.Click.inProgress;
        skipText.gameObject.SetActive(holdLbm);
        
        _skipTimer = holdLbm
            ? Mathf.Clamp(_skipTimer + Time.deltaTime, 0, holdTimer)
            : Mathf.Clamp(_skipTimer - Time.deltaTime, 0, holdTimer);

        if (_skipTimer >= holdTimer)
        {
            EventManager.Publish("StartGame");
        }
    }

    private void OnDestroy()
    {
        _inputSystem.UI.Disable();
    }
}
