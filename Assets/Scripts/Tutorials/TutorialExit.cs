using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialExit: MonoBehaviour, IInteractable
{
    public void Interact()
    {
        SceneManager.LoadScene(2);
    }
}