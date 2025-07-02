using TMPro;
using UnityEngine;

public class InputPinTerminal : MonoBehaviour
{
    [SerializeField] private TMP_Text pinText;
    [SerializeField] private TerminalInteract terminalInteract;
    [SerializeField] private string pinCode = "1111";
    [SerializeField] private int maxPinLength = 4;

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
            // AudioSource
            pinText.text = "OPEN";
            EventManager.Publish("OpenDoorForTerminal");
            terminalInteract.Exit();
        }
        else
        {
            pinText.text = "";
            // AudioSource
        }
    }
}
