using System.Collections;
using UnityEngine;

namespace Cutscene
{
    public class NextSlide : MonoBehaviour
    {
        [SerializeField] private GameObject[] slides;
        [SerializeField] private float slideTime;

        private IEnumerator Start()
        {
            var slideNumber = 0;
            while (slideNumber < slides.Length)
            {
                if(slideNumber > 0)
                    slides[slideNumber - 1].SetActive(false);
                
                slides[slideNumber].SetActive(true);
                slideNumber++;
                
                yield return new WaitForSeconds(slideTime);
            }
            EventManager.Publish("StartGame");
        }
    }
}