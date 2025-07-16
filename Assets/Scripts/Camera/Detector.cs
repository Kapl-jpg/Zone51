using Enums;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private Transform sphereCenter;
    [SerializeField] private AudioSource[] audioAlarms;
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private float sphereRadius = 5f;

    private Transform detectedPlayer;
    private AudioSource soundReproducing;

    private bool isDetecting = false;
    private bool isSoundPlaying = false;
    private bool isSoundCompleted = false;
    [SerializeField] private bool isSwitch = true;
    private bool isOver = false;

    private void Update()
    {
        if (isOver) return;

        CheckSphereCast();

        if (!isSoundCompleted)
        {
            UpdateDetection();
        }

        if (isSoundPlaying && !soundReproducing.isPlaying)
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
            if (isSwitch)
            {
                AppointmentAudio();
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
            ResetDetection();
        }
    }

    private bool CheckLineOfSight()
    {
        if (rayOrigin == null || detectedPlayer == null) return false;

        Vector3 direction = (detectedPlayer.position - rayOrigin.position).normalized;
        float distance = Vector3.Distance(rayOrigin.position, detectedPlayer.position);

        if (Physics.Raycast(rayOrigin.position, direction, out RaycastHit obstacleHit, distance, obstacleLayerMask))
        {
            Debug.DrawRay(rayOrigin.position, direction * obstacleHit.distance, Color.yellow, 0.1f);
            return false;
        }

        if (Physics.Raycast(rayOrigin.position, direction, out RaycastHit playerHit, distance, detectionLayer))
        {
            var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
            if (characterType == CharacterType.Alien)
            {
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
        // ƒополнительные действи€ при проигрыше
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

    private void AppointmentAudio()
    {
        int countSound = Random.Range(0, audioAlarms.Length);
        soundReproducing = audioAlarms[countSound];
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

