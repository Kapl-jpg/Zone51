using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cutscene
{
    public class Lose :Subscriber
    {
        [SerializeField] private int loseSceneIndex;
        [SerializeField] private float delay;
        private bool _lose;
        
        [Event("Lose")]
        private void EndGame()
        {
            if(!_lose)
                StartCoroutine(LoadLoseScene());
        }

        private IEnumerator LoadLoseScene()
        {
            _lose = true;
            print("Loading Lose Scene");
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(loseSceneIndex);
        }
            
    }
}