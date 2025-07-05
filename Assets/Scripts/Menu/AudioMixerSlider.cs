using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerSlider : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private string mixerParameter;
    [SerializeField] private float minVolumeDb;
    [SerializeField] private float maxVolumeDb = 0f;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(UpdateMixerVolume);
        volumeSlider.value = GetNormalizedVolume();
    }

    private float GetNormalizedVolume()
    {
        float dbVolume = AudioManager.Instance.GetMixerVolume(mixerParameter);
        return Mathf.InverseLerp(minVolumeDb, maxVolumeDb, dbVolume);
    }

    public void UpdateMixerVolume(float normalizedVolume)
    {
        float dbVolume = Mathf.Lerp(minVolumeDb, maxVolumeDb, normalizedVolume);
        AudioManager.Instance.SetMixerVolume(mixerParameter, dbVolume);
    }
}
