using UnityEngine;

public class OnAudioUpdateClassic : MonoBehaviour
{
    private AudioSource _audioUpdate;

    private bool _isPlayingAudio = false;

    private void Start()
    {
        _audioUpdate = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_isPlayingAudio && !_audioUpdate.isPlaying)
        {
            print("Play");
            _audioUpdate.Play();
            _isPlayingAudio = false;
        }
    }

    [Event("OnAudioUpdate")]
    private void OnAudioUpdate()
    {
        _isPlayingAudio = true;
    }
}
