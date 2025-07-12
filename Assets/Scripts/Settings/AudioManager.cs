using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Settings/AudioManager" , fileName = "AudioManager")]
public class AudioManager : ScriptableObject
{
    [SerializeField] private AudioMixer audioMixer;
    private float _effectsVolume;
    private float _musicVolume;

    public float EffectsVolume => _effectsVolume;
    public float MusicVolume => _musicVolume;

    public void SetEffectsVolume(float volume)
    {
        _effectsVolume = volume;
        audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(volume > 0 ? -40f : -80f, 0f, volume));
    }

    public void SetMusicVolume(float volume)
    {
        _musicVolume = volume;
        audioMixer.SetFloat("MusicVolume", Mathf.Lerp(volume > 0 ? -40f : -80f, 0f, volume));
    }
}