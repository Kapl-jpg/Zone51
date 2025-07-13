using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class AudioSettings: MonoBehaviour
    {
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectsSlider;

        private void Awake()
        {
            audioManager.SetMusicVolume(audioManager.MusicVolume);
            audioManager.SetEffectsVolume(audioManager.EffectsVolume);
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