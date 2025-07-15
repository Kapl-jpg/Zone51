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
    private bool activeAudio = true;
    private bool isSwitch = true;
    private bool isOver = false;

    private void Start()
    {
        AppointmentAudio();
    }

    private void Update()
    {
        print(soundReproducing);
        CheckSphereCast();
        UpdateDetection(); 
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
                if (!isDetecting)
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
            AppointmentAudio();

            if (!soundReproducing.isPlaying && activeAudio)
            {
                soundReproducing.Play();
                activeAudio = false;
            } 
            else if (!soundReproducing.isPlaying)
            {
                PlayerFullyDetected();
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
                bool isPlayer = playerHit.collider.CompareTag("Player");
                Debug.DrawRay(rayOrigin.position, direction * playerHit.distance, isPlayer ? Color.green : Color.red, 0.1f);
                return isPlayer;
            }
        }

        return false;
    }

    private void PlayerFullyDetected() // GameOver
    {
        //if (soundReproducing.isPlaying)
        //{
        //    soundReproducing.Stop();
        //}

        //activeAudio = true; // Commit

        isOver = true;
        isSwitch = true;
        Debug.Log("GameOver");
        ResetDetection();
        //Time.timeScale = 0;
    }

    private void ResetDetection()
    {
        if (soundReproducing.isPlaying && !isOver)
        {
            soundReproducing.Stop();
            activeAudio = true;
        }

        isDetecting = false;
        detectedPlayer = null;
    }

    private void AppointmentAudio()
    {
        if (isSwitch)
        {
            int countSound = Random.Range(0, audioAlarms.Length);
            //int countSound = 1;
            soundReproducing = audioAlarms[countSound];
            isSwitch = false;
            print("Swith");
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

