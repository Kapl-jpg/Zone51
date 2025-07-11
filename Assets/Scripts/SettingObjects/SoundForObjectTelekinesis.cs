using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))] // Автоматически добавляет Rigidbody, если его нет
[RequireComponent(typeof(AudioSource))] // Автоматически добавляет AudioSource, если его нет

public class SoundForObjectTelekinesis : MonoBehaviour
{
    // Ссылки на компоненты, которые будут автоматически найдены при старте
    private AudioSource audioSource;
    private Rigidbody rb;

    [Header("Настройки силы удара")]
    [Tooltip("Минимальная скорость удара, при которой будет воспроизводиться звук. Ниже этого значения звук не будет проигрываться.")]
    [Range(0.1f, 10.0f)]
    public float minImpactVelocity = 1.0f;
    [Tooltip("Скорость удара, при которой звук будет воспроизводиться на максимальной громкости (maxVolume).")]
    [Range(1.0f, 20.0f)]
    public float maxImpactVelocity = 10.0f;

    [Header("Настройки громкости")]
    [Tooltip("Минимальная громкость звука при ударе (для minImpactVelocity).")]
    [Range(0.0f, 1.0f)]
    public float minVolume = 0.1f;
    [Tooltip("Максимальная громкость звука при ударе (для maxImpactVelocity).")]
    [Range(0.0f, 1.0f)]
    public float maxVolume = 1.0f;

    void Awake()
    {
        // Получаем ссылки на компоненты на этом же GameObject
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        // Убедимся, что Play On Awake выключен, чтобы звук не играл сразу при запуске
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
        else
        {
            Debug.LogError("На объекте '" + gameObject.name + "' не найден компонент AudioSource. Скрипт не будет работать без него.", this);
            enabled = false; // Отключаем скрипт, если нет AudioSource
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Проверяем, что AudioSource существует и что у него назначен аудиоклип
        if (audioSource == null || audioSource.clip == null)
        {
            // Debug.LogWarning("AudioSource или его AudioClip не назначен. Звук удара не будет воспроизведен.", this);
            return;
        }

        // Получаем магнитуду (величину) относительной скорости столкновения
        // Это хорошая мера "силы" удара.
        float impactMagnitude = collision.relativeVelocity.magnitude;

        // Если сила удара слишком мала, не воспроизводим звук
        if (impactMagnitude < minImpactVelocity)
        {
            return;
        }

        // Нормализуем силу удара в диапазон от 0 до 1, используя min/max ImpactVelocity
        // Mathf.InverseLerp: возвращает значение от 0 до 1, показывающее, где value находится между a и b.
        float normalizedImpact = Mathf.InverseLerp(minImpactVelocity, maxImpactVelocity, impactMagnitude);

        // Масштабируем нормализованную силу удара в диапазон громкости (от minVolume до maxVolume)
        // Mathf.Lerp: интерполирует между a и b на основе t (где t от 0 до 1).
        float finalVolume = Mathf.Lerp(minVolume, maxVolume, normalizedImpact);

        // Устанавливаем громкость AudioSource
        audioSource.volume = finalVolume;

        // Воспроизводим звук
        // PlayOneShot() предпочтительнее Play(), так как он не прерывает текущий звук
        // и может воспроизводить несколько звуков поверх друг друга на одном AudioSource.
        audioSource.PlayOneShot(audioSource.clip);

        // Опционально: отладочные сообщения
        // Debug.Log($"Объект '{gameObject.name}' столкнулся с силой: {impactMagnitude:F2}. Громкость звука: {finalVolume:F2}.", this);
    }
}