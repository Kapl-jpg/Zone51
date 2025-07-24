using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchNextScreen : MonoBehaviour
{
    [SerializeField] private GameObject currentScreen;
    [SerializeField] private GameObject nextScreen;
    [SerializeField] private float delay;
    [SerializeField] private float loadSceneDelay;
    [SerializeField] private int loseSceneIndex;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        currentScreen.SetActive(false);
        nextScreen.SetActive(true);
        yield return new WaitForSeconds(loadSceneDelay);
        SceneManager.LoadScene(loseSceneIndex);
    }
}