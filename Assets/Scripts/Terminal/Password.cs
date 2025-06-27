using System;
using System.Collections;
using System.Collections.Generic;
using Terminal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    [SerializeField] private int password;
    [SerializeField] private int numberAttempts;
    
    [SerializeField] private float waitTransitionTime;
    
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color winColor;
    [SerializeField] private Color loseColor;
    
    [SerializeField] private Image authorizationScreenBackground;
    
    [SerializeField] private TMP_Text attemptsText;
    
    [SerializeField] private TMP_Text firstWindow;
    [SerializeField] private TMP_Text secondWindow;
    [SerializeField] private TMP_Text thirdWindow;
    [SerializeField] private TMP_Text fourthWindow;

    [SerializeField] private List<Button> buttons;
    
    [SerializeField] private GameObject currentScreen;
    [SerializeField] private GameObject nextScreen;
    
    [SerializeField] private List<AttemptData> attempts;
    
    private int _numberAttempts;
    private readonly List<int> _numbers = new();

    private void Start()
    {
        _numberAttempts = attempts.Count;
        ShowAttempts();
    }

    public void AddNumber(int number)
    {
        _numbers.Add(number);
        
        UpdateNumber();
        
        if (_numbers.Count >= 4)
        {
            CheckNextScene();
        }
    }

    public void ClearNumber()
    {
        if(_numbers.Count > 0)
            _numbers.RemoveAt(_numbers.Count - 1);
        
        UpdateNumber();
    }

    private void UpdateNumber()
    {
        for (int i = 0; i < 4; i++)
        {
            if(i <= _numbers.Count - 1)
                FieldByIndex(i).text = _numbers[i].ToString();
            else
                FieldByIndex(i).text = String.Empty;
        }
    }

    private void CheckNextScene()
    {
        var checkNumber = 0;
        
        foreach (var number in _numbers)
            checkNumber = checkNumber * 10 + number;

        StartCoroutine(checkNumber == password ? WinScreen() : LoseScreen());

        _numbers.Clear();
    }

    private IEnumerator WinScreen()
    {
        authorizationScreenBackground.color = winColor;
        yield return new WaitForSeconds(waitTransitionTime);
        nextScreen.SetActive(true);
        currentScreen.SetActive(false);
    }

    private IEnumerator LoseScreen()
    {
        authorizationScreenBackground.color = loseColor;
        
        _numberAttempts--;
        ShowAttempts();
        
        foreach (var button in buttons)
            button.interactable = false;
        
        yield return new WaitForSeconds(waitTransitionTime);
        
        foreach (var button in buttons)
            button.interactable = true;
        
        authorizationScreenBackground.color = defaultColor;
        
        firstWindow.text = String.Empty;
        secondWindow.text = String.Empty;
        thirdWindow.text = String.Empty;
        fourthWindow.text = String.Empty;

        if (_numberAttempts <= 0)
            print("Lose");
    }

    private void ShowAttempts()
    {
        if (_numberAttempts > 0)
            attemptsText.text = attempts.Find(x => x.attempt == _numberAttempts).description;
    }

    private TMP_Text FieldByIndex(int index)
    {
        switch (index)
        {
            case 0:
                return firstWindow;
            case 1:
                return secondWindow;
            case 2:
                return thirdWindow;
            case 3:
                return fourthWindow;
            default:
                return null;
        }
    }
}
