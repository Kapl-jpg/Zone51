using System;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class AudioSettings: MonoBehaviour
    {
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectsSlider;

        private void Start()
        {
            var music = ES3.Load<float>("MusicVolume");
            var effects = ES3.Load<float>("EffectsVolume");
            musicSlider.value = music;
            effectsSlider.value = effects;
        }

        public void ChangeMusic()
        {
            audioManager.SetMusicVolume(musicSlider.value);
        }
        
        public void ChangeEffects()
        {
            audioManager.SetEffectsVolume(effectsSlider.value);
        }
    }
}