using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadTutorialScene()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadComicsScene()
    {
        SceneManager.LoadScene(2);
    }
}
