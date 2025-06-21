using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class DoorsOnCard : MonoBehaviour
{
    [SerializeField] private SettingAnimations settingAnimationsDoor1;
    [SerializeField] private SettingAnimations settingAnimationsDoor2;
    [SerializeField] private InputMeneger inputMeneger;
    [SerializeField] private GameObject interactionButton;
    [SerializeField] private GameObject cardInPlayer;
    [SerializeField] private GameObject cardInTerminal;

    private void Update()
    {
        if (inputMeneger.InputE() && interactionButton.activeSelf)
        {
            settingAnimationsDoor1.ActiveAnimationsDoor(true);
            settingAnimationsDoor2.ActiveAnimationsDoor(true);
            cardInPlayer.SetActive(false);
            cardInTerminal.SetActive(true);
        }
    }
}
