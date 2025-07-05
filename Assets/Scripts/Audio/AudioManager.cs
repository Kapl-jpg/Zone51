using Patterns.Singleton;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixer audioMixer;

    public float GetMixerVolume(string parameterName)
    {
        audioMixer.GetFloat(parameterName, out float volume);
        return volume;
    }

    public void SetMixerVolume(string parameterName, float volumeDb)
    {
        audioMixer.SetFloat(parameterName, volumeDb);
    }
}
