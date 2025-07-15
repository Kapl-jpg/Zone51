using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour, IInteractable
{
    [SerializeField] private int nextSceneIndex;
    public void Interact()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
