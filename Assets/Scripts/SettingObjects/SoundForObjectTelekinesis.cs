using UnityEngine;

public class SoundForObjectTelekinesis : MonoBehaviour
{
    private AudioSource audioSource;
    private Rigidbody rb;

    [Header("Настройки силы удара")]

    [Range(0.1f, 10.0f)]
    public float minImpactVelocity = 1.0f;

    [Range(1.0f, 20.0f)]
    public float maxImpactVelocity = 10.0f;

    [Header("Настройки громкости")]

    [Range(0.0f, 1.0f)]
    public float minVolume = 0.1f;

    [Range(0.0f, 1.0f)]
    public float maxVolume = 1.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (audioSource == null || audioSource.clip == null)
        {
            return;
        }

        float impactMagnitude = collision.relativeVelocity.magnitude;

        if (impactMagnitude < minImpactVelocity)
        {
            return;
        }

        float normalizedImpact = Mathf.InverseLerp(minImpactVelocity, maxImpactVelocity, impactMagnitude);
        float finalVolume = Mathf.Lerp(minVolume, maxVolume, normalizedImpact);

        audioSource.volume = finalVolume;
        audioSource.PlayOneShot(audioSource.clip);

    }
}