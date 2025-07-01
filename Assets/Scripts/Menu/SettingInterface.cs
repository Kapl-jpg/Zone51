using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingInterface : MonoBehaviour
{
    [SerializeField] private SettingKeys settingKeys;
    [SerializeField] private GameObject settingMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject exitMenu;
    [SerializeField] private GameObject imageExit;

    //private AudioSource audioSource;
    private bool activebTools;

    private void Start()
    {
       // audioSource = GetComponent<AudioSource>();
        activebTools = false;
    }

    public void ButtonPlay()
    {
        //audioSource.Play();
        StartCoroutine(LoadNextSceneAfterTime(0.1f, 1));
    }

    public void ButtonInstruction()
    {
        //audioSource.Play();
        StartCoroutine(LoadNextSceneAfterTime(0.1f, 2));
    }

    public void ButtonInMenuExit()
    {
        //audioSource.Play();
        mainMenu.SetActive(false);
        exitMenu.SetActive(true);
        imageExit.SetActive(true);
    }

    public void ButtonExit()
    {
        StartCoroutine(ExitTheGame(0.1f));
    }

    public void ButtonInMenu()
    {
        //audioSource.Play();
        Time.timeScale = 1;
        StartCoroutine(LoadNextSceneAfterTime(0.1f, 0));
    }

    public void ButtonBack()
    {
        BackInMenu();
    }

    public void ButtonContinue()
    {
        //audioSource.Play();
        //settingKeys.PauseMode();
    }

    public void ButtonSetting()
    {
        mainMenu.SetActive(false);
        settingMenu.SetActive(true);
    }

    IEnumerator LoadNextSceneAfterTime(float time, int index)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(index);
    }

    IEnumerator ExitTheGame(float time)
    {
        yield return new WaitForSeconds(time);
        Application.Quit();
    }

    IEnumerator ContinueGame(float time)
    {
        yield return new WaitForSeconds(time);
        //settingKeys.PauseMode();
    }

    private void BackInMenu()
    {
        mainMenu.SetActive(true);
        settingMenu.SetActive(false);

        if (exitMenu.activeInHierarchy)
        {
            imageExit.SetActive(false);
            exitMenu.SetActive(false);
        }
    }
}
