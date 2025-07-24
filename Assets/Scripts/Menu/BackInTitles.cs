using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BackInTitles : MonoBehaviour
{
    [SerializeField] private GameObject menuTitles;
    [SerializeField] private GameObject menuSetting;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private InputManager inputManager;

    private void Update()
    {
        BackESC();
    }

    private void BackESC()
    {
        if (menuTitles.activeSelf && inputManager.InputPause())
        {
            menuTitles.SetActive(false);
            mainMenu.SetActive(true);
        }

        if (menuSetting.activeSelf && inputManager.InputPause())
        {
            menuSetting.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}
