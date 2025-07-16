using UnityEngine;

public class OnAudioUpdateClassic : MonoBehaviour
{
    private AudioSource _audioUpdate;

    private void Start()
    {
        _audioUpdate = GetComponent<AudioSource>();
    }

    [Event("OnAudioUpdate")]
    private void OnAudioUpdate()
    {
        if (!_audioUpdate.isPlaying)
        {
            _audioUpdate.Play();
        }
    }
}
