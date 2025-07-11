using UnityEngine;

public class TutorialInput : MonoBehaviour
{
    private InputSystem_Actions _inputSystem;

    private void Start()
    {
        _inputSystem = new InputSystem_Actions();
        _inputSystem.UI.Enable();
    }

    public bool SkipTutorial() => _inputSystem.UI.SkipTutorial.triggered;
}