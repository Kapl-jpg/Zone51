using Enums;
using UnityEngine;

public class Detector : Subscriber
{
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private Transform sphereCenter;
    [SerializeField] private AudioSource[] audioAlarms; // Массив звуков сигнализации
    [SerializeField] private AudioSource[] audioDoctors; // Массив звуков для доктора
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private FlickerLight flickerLight;
    [SerializeField] private float sphereRadius = 5f;

    private Transform detectedPlayer;
    private AudioSource soundReproducing;
    private AudioSource doctorSoundReproducing;

    private AudioSource audioForPauseAlarm;
    private AudioSource audioForPauseForDoctor;

    private bool isDetecting = false;
    private bool isSoundPlaying = false;
    private bool isSoundCompleted = false;
    private bool isSwitch = true;
    private bool isOver = false;
    private bool activeAudioForPlayer = false;
    private bool isLoss = true;
    private bool isCameraLook = false;

    private void Update()
    {
        if (isOver) return;

        CheckSphereCast();

        if (!isSoundCompleted)
        {
            UpdateDetection();
        }

        if (isSoundPlaying && !soundReproducing.isPlaying && isLoss)
        {
            if (isDetecting)
            {
                PlayerFullyDetected();
            }
            else
            {
                isSoundPlaying = false;
                isSoundCompleted = false;
                isSwitch = true;
            }
        }

        
    }

    private void CheckSphereCast()
    {
        Collider[] hitColliders = Physics.OverlapSphere(sphereCenter.position, sphereRadius, detectionLayer);
        bool playerInZone = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                playerInZone = true;
                if (!isDetecting && !isSoundCompleted)
                {
                    detectedPlayer = hitCollider.transform;
                    isDetecting = true;
                }
                break;
            }
        }

        if (!playerInZone && isDetecting)
        {
            ResetDetection();
        }
    }

    private void UpdateDetection()
    {
        if (!isDetecting || detectedPlayer == null) return;

        if (CheckLineOfSight())
        {
            if (doctorSoundReproducing != null)
            {
                doctorSoundReproducing.Stop();
            }
            

            if (isSwitch)
            {
                AppointmentAudioAlien();
            }

            if (!isSoundPlaying && soundReproducing != null)
            {
                soundReproducing.Play();
                isSoundPlaying = true;
                isSoundCompleted = false;
                isSwitch = false;
            }
        }
        else
        {
            if (isSoundPlaying && soundReproducing != null)
            {
                soundReproducing.Stop();
                isSoundPlaying = false;
                isSwitch = true;
            }

            if (activeAudioForPlayer)
            {
                Vector3 direction = (detectedPlayer.position - rayOrigin.position).normalized;
                float distance = Vector3.Distance(rayOrigin.position, detectedPlayer.position);

                if (Physics.Raycast(rayOrigin.position, direction, out RaycastHit playerHit, distance, detectionLayer))
                {
                    var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
                    if (characterType == CharacterType.Human)
                    {
                        ResetDetection();
                        flickerLight.FlashingLights(false);

                        if (audioDoctors.Length > 0 && (doctorSoundReproducing == null || !doctorSoundReproducing.isPlaying))
                        {
                            AppointmentAudioDoctor();
                            doctorSoundReproducing.Play();
                            activeAudioForPlayer = false;
                        }
                    }
                }
            }
            else
            {
                ResetDetection();
            }
        }
    }

    private bool CheckLineOfSight()
    {
        if (rayOrigin == null || detectedPlayer == null) return false;

        Vector3 direction = (detectedPlayer.position - rayOrigin.position).normalized;
        float distance = Vector3.Distance(rayOrigin.position, detectedPlayer.position);

        if (Physics.Raycast(rayOrigin.position, direction, out RaycastHit obstacleHit, distance, obstacleLayerMask))
        {
            flickerLight.FlashingLights(false);
            Debug.DrawRay(rayOrigin.position, direction * obstacleHit.distance, Color.yellow, 0.1f);
            return false;
        }

        if (Physics.Raycast(rayOrigin.position, direction, out RaycastHit playerHit, distance, detectionLayer))
        {
            var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
            if (characterType == CharacterType.Alien)
            {
                flickerLight.FlashingLights(true);
                activeAudioForPlayer = true;
                isCameraLook = true;

                Debug.DrawRay(rayOrigin.position, direction * playerHit.distance, Color.green, 0.1f);
                return playerHit.collider.CompareTag("Player");
            }
        }

        return false;
    }

    private void PlayerFullyDetected()
    {
        isOver = true;
        isSoundCompleted = true;
        EventManager.Publish("Lose");
    }

    private void ResetDetection()
    {
        if (isSoundPlaying && soundReproducing != null)
        {
            soundReproducing.Stop();
            isSoundPlaying = false;
        }

        isDetecting = false;
        detectedPlayer = null;

        if (!isSoundCompleted)
        {
            isSwitch = true;
        }
    }

    private void AppointmentAudioAlien()
    {
        int countSound = Random.Range(0, audioAlarms.Length);
        soundReproducing = audioAlarms[countSound];
    }

    private void AppointmentAudioDoctor()
    {
        int randomDoctorSound = Random.Range(0, audioDoctors.Length);
        doctorSoundReproducing = audioDoctors[randomDoctorSound];
    }

    [Event("IsPauseAllAudioInCamera")]
    private void IsPauseAllAudioInCamera(bool active)
    {
        if (active)
        {
            if (soundReproducing != null && soundReproducing.isPlaying)
            {
                soundReproducing.Pause();
                audioForPauseAlarm = soundReproducing;
            }

            if (doctorSoundReproducing != null && doctorSoundReproducing.isPlaying)
            {
                doctorSoundReproducing.Pause();
                audioForPauseForDoctor = doctorSoundReproducing;
            }

            isLoss = false;
        }
        else
        {
            if (audioForPauseAlarm != null)
            {
                soundReproducing.Play();
                audioForPauseAlarm = null;
            }

            if (audioForPauseForDoctor != null)
            {
                audioForPauseForDoctor.Play();
                audioForPauseForDoctor = null;
            }

            isLoss = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (sphereCenter != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(sphereCenter.position, sphereRadius);
        }
    }
}