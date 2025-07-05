using System;
using System.Collections;
using System.Collections.Generic;
using Patterns.Singleton;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private float minSensitivity = 0.1f;
    [SerializeField] private float maxSensitivity = 10f;
    [SerializeField] private float defaultSensitivity = 1f;
    //[SerializeField] private PlayerRotate playerRotate;

    public event Action<float> OnSensitivityChanged;
    private void Start()
    {

        sensitivitySlider.minValue = minSensitivity;
        sensitivitySlider.maxValue = maxSensitivity;
        sensitivitySlider.value = defaultSensitivity;

        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
        //Загрузка значения из PlayerPrefs (опционально)
        LoadSensitivity();
    }

    private void UpdateSensitivity(float value)
    {
        OnSensitivityChanged?.Invoke(value);
        Debug.Log("Sensitivity changed to: " + value);

    }
    private void LoadSensitivity()
    {
        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
            UpdateSensitivity(PlayerPrefs.GetFloat("MouseSensitivity"));
        }
        else
        {
            PlayerPrefs.SetFloat("MouseSensitivity", defaultSensitivity);
        }
    }

    private void OnApplicationQuit()
    {
        //Сохраняем чувствительность в PlayerPrefs при выходе из приложения (опционально)
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivitySlider.value);
    }
}
