using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : Subscriber
{
    [SerializeField] private int nextSceneIndex;
    [Event("StartGame")]
    public void LoadScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
