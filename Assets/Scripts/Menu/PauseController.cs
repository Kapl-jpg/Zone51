using UnityEngine;
using UnityEngine.EventSystems;

public class PauseController : Subscriber
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject settingsWindow;
    [SerializeField] private GameObject menuPause;

    private bool _activeMenuPause;

    private void Update()
    {
        if (inputManager.InputPause())
        {
            PauseMode();
        }
    }

    [Event("PauseMode")]
    private void PauseMode()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (settingsWindow.activeInHierarchy)
        {
            settingsWindow.SetActive(false);
            menuPause.SetActive(true);
            return;
        }
        
        _activeMenuPause = !_activeMenuPause;
        
        if (_activeMenuPause)
        {
            menuPause.SetActive(true);
            EventManager.Publish("DisableTip");
            EventManager.Publish("OnOffCursor", true);
            EventManager.Publish("HideCrosshair");
            EventManager.Publish("IsPauseAllAudioInCamera", true);
            Time.timeScale = 0;
        }
        else
        {
            menuPause.SetActive(false);
            EventManager.Publish("EnableTip");
            EventManager.Publish("OnOffCursor", false);
            EventManager.Publish("ShowCrosshair");
            EventManager.Publish("IsPauseAllAudioInCamera", false);
            Time.timeScale = 1;
        }
    }
}
