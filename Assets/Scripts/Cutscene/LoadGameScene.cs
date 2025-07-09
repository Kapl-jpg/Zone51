using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : Subscriber
{
    [Event("StartGame")]
    public void LoadScene()
    {
        SceneManager.LoadScene(2);
    }
}
