using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour, IFinishable
{
    [SerializeField] private int nextSceneIndex;
    
    public void Finish()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
