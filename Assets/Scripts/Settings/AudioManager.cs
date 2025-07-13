using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Settings/AudioManager" , fileName = "AudioManager")]
public class AudioManager : ScriptableObject
{
    [SerializeField] private AudioMixer audioMixer;
    private float _effectsVolume = 1f;
    private float _musicVolume = 1f;

    public float EffectsVolume => _effectsVolume;
    public float MusicVolume => _musicVolume;

    public void SetEffectsVolume(float volume)
    {
        _effectsVolume = volume;
        audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(volume > 0 ? -40f : -80f, 0f, volume));
        ES3.Save("EffectsVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        _musicVolume = volume;
        audioMixer.SetFloat("MusicVolume", Mathf.Lerp(volume > 0 ? -40f : -80f, 0f, volume));
        ES3.Save("MusicVolume", volume);
    }
}