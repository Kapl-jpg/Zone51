using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSlider : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private float minSensitivity = 0.1f; // ћинимальна€ чувствительность
    [SerializeField] private float maxSensitivity = 10f; // ћаксимальна€ чувствительность
    [SerializeField] private float defaultSensitivity = 1f; // «начение по умолчанию

    public float Sensitivity { get; private set; } = 1f; // “екуща€ чувствительность

    private void Start()
    {
        // ”станавливаем значение слайдера по умолчанию
        sensitivitySlider.minValue = minSensitivity;
        sensitivitySlider.maxValue = maxSensitivity;
        sensitivitySlider.value = defaultSensitivity;
        Sensitivity = defaultSensitivity;


        // ѕодписываемс€ на событие изменени€ значени€ слайдера
        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);

        //«агружаем чувствительность из PlayerPrefs (опционально)
        LoadSensitivity();
    }

    private void UpdateSensitivity(float value)
    {
        Sensitivity = value;
        Debug.Log("Mouse Sensitivity: " + Sensitivity);
        // «десь вы должны добавить код дл€ применени€ Sensitivity к вашей игре
        // Ќапример, изменить скорость вращени€ камеры.
    }


    private void LoadSensitivity()
    {
        // «агрузить сохраненную чувствительность из PlayerPrefs
        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
            UpdateSensitivity(PlayerPrefs.GetFloat("MouseSensitivity"));
        }
        else
        {
            // ”становить значение по умолчанию, если сохранение отсутствует.
            PlayerPrefs.SetFloat("MouseSensitivity", defaultSensitivity);
        }
    }


    private void OnApplicationQuit()
    {
        //—охран€ем чувствительность в PlayerPrefs при выходе из приложени€ (опционально)
        PlayerPrefs.SetFloat("MouseSensitivity", Sensitivity);
    }
}
