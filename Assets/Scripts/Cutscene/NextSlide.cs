using UnityEngine;

namespace Cutscene
{
    public class NextSlide : Subscriber
    {
        [SerializeField] private GameObject[] slides;
        [SerializeField] private float slideTime;
        private int _slideNumber;

        [Event("NextSlide")]
        private void Next()
        {
            _slideNumber++;
            if (_slideNumber < slides.Length)
            {
                slides[_slideNumber - 1].SetActive(false);
                slides[_slideNumber].SetActive(true);
            }
            else
            {
                EventManager.Publish("StartGame");
            }
        }
    }
}