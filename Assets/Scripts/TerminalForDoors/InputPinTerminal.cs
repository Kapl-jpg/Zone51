using TMPro;
using UnityEngine;

public class InputPinTerminal : MonoBehaviour
{
    [SerializeField] private TMP_Text pinText;
    [SerializeField] private TerminalInteract terminalInteract;
    [SerializeField] private AudioSource audioOpen;
    [SerializeField] private AudioSource audioClose;
    [SerializeField] private string pinCode = "1111";
    [SerializeField] private int maxPinLength = 4;
    [SerializeField] private GameObject terminal40;

    public void InputKeyForCode(string key)
    {
        pinText.text = (pinText.text + key).Substring(0, Mathf.Min(pinText.text.Length + key.Length, maxPinLength));
    }

    public void EraseCode()
    {
        string textCode = pinText.text;
        
        if (textCode != null)
        {
            pinText.text = pinText.text.Remove(textCode.Length - 1);
        }
    }

    public void CheckingCode()
    {
        string textCode = pinText.text;
        
        if (textCode == pinCode)
        {
            audioOpen.Play();
            pinText.text = "OPEN";
            EventManager.Publish("OpenDoorForTerminal");
            EventManager.Publish("DropInteraction");
            terminal40.SetActive(true);
            terminalInteract.DisableTerminal();
            terminalInteract.Exit();
        }
        else
        {
            pinText.text = "";
            audioClose.Play();
        }
    }
}
