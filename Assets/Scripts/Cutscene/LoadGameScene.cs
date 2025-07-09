using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : Subscriber
{
    [SerializeField] private SceneAsset scene;

    [Event("StartGame")]
    public void LoadScene()
    {
        SceneManager.LoadScene(scene.name);
    }
}
