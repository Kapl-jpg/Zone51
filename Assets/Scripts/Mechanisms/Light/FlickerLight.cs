using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    [SerializeField] private float flickerDelay = 0.5f;  // Задержка между переключениями (сек)
    [SerializeField] private float lightIntensity = 1f; // Фиксированная яркость (когда свет включен)

    private Light lightSource;
    private float nextToggleTime;
    private bool isLightOn = true;

    private void Start()
    {
        lightSource = GetComponent<Light>();

        nextToggleTime = Time.time + flickerDelay;
        lightSource.intensity = lightIntensity; // Начальная яркость
    }

    [Event("FlashingLights")]
    private void FlashingLights()
    {
        if (Time.time >= nextToggleTime)
        {
            isLightOn = !isLightOn; // Переключаем состояние
            lightSource.intensity = isLightOn ? lightIntensity : 0f; // Вкл/Выкл

            nextToggleTime = Time.time + flickerDelay; // Обновляем время следующего переключения
        }
    }
}
