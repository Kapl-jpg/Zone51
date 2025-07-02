using UnityEngine;

public class PauseController : Subscriber
{
    [SerializeField] private InputMeneger inputMeneger;
    [SerializeField] private GameObject menuPause;

    private bool _activeMenuPause;

    private void Update()
    {
        if (inputMeneger.InputPause())
        {
            PauseMode();
        }
    }

    [Event("PauseMode")]

    private void PauseMode()
    {
        _activeMenuPause = !_activeMenuPause;

        if (_activeMenuPause)
        {
            menuPause.SetActive(true);
            EventManager.Publish("OnOffCursor", true);
            Time.timeScale = 0;
        }
        else
        {
            menuPause.SetActive(false);
            EventManager.Publish("OnOffCursor", false);
            Time.timeScale = 1;
        }
    }
}
