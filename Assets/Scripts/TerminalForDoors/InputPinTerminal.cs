using UnityEngine;
using UnityEngine.UI;

public class InputPinTerminal : MonoBehaviour
{
    [SerializeField] private Text pinText;
    [SerializeField] private string pinCode = "1111";


    public void InputKeyForCode(string key)
    {
        pinText.text += key;
        print("print");
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
            //EventManager.Publish("OpenMainDoor");
        }
        else
        {
            pinText.text = "";
            // AudioSource
        }
    }
}
