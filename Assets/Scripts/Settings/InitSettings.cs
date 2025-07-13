using System;
using Settings;
using UnityEngine;

public class InitSettings : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private MouseManager mouseManager;

    private void Awake()
    {
        var music = ES3.Load<float>("MusicVolume");
        var effects = ES3.Load<float>("EffectsVolume");
        var sensitivity = ES3.Load<float>("MouseSensitivity");
        
        audioManager.SetMusicVolume(music);
        audioManager.SetEffectsVolume(effects);
        mouseManager.SetMouseSensitivity(sensitivity);
    }
}