using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DataAudiWithCamera : MonoBehaviour 
{
    private AudioSource audioSourcesCamera;

    public void AddedAudioInArray(AudioSource audio)
    {
        audioSourcesCamera = audio;
    }

    public void OffAudioInArray()
    {
        if (audioSourcesCamera != null)
        {
            if (audioSourcesCamera.isPlaying)
            {
                audioSourcesCamera.Stop();
                audioSourcesCamera = null;
            }
        }
    }
}
