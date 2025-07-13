using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Detector : MonoBehaviour
{
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private Transform sphereCenter;
    [SerializeField] private GameObject odjectText;
    [SerializeField] private TMP_Text textTimer;
    [SerializeField] private AudioSource audioAlarm;
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private float sphereRadius = 5f;
    [SerializeField] private float maxDetectionTime = 5f;

    private Transform detectedPlayer;
    private float currentDetectionTime;
    private bool isDetecting = false;
    private bool activeTimer;
    

    private void Start()
    {
        currentDetectionTime = maxDetectionTime;
    }

    private void Update()
    {
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

        // Если игрок вышел из зоны или исчез - сброс
        if (!playerInZone && isDetecting)
        {
            ResetDetection();
        }
    }

    private void UpdateDetection()
    {
        if (!isDetecting || detectedPlayer == null) return;

        activeTimer = true;
        if (CheckLineOfSight() && activeTimer)
        {
            //audioAlarm.Play();
            currentDetectionTime -= Time.deltaTime;
            odjectText.SetActive(true);
            textTimer.text = currentDetectionTime.ToString("0:00");
            //Debug.Log($"Обнаружение: {currentDetectionTime}");

            if (currentDetectionTime <= 0)
            {
                PlayerFullyDetected();
            }
        }
        else
        {
            //currentDetectionTime = Mathf.Max(0, currentDetectionTime - Time.deltaTime * 0.5f); // Медленный сброс
            ResetDetection();
            //odjectText.SetActive(false);
        }
    }

    private bool CheckLineOfSight()
    {
        if (rayOrigin == null || detectedPlayer == null) return false;

        Vector3 direction = (detectedPlayer.position - rayOrigin.position).normalized;
        float distance = Vector3.Distance(rayOrigin.position, detectedPlayer.position);
        Debug.DrawRay(rayOrigin.position, direction * distance, Color.red, 0.1f);
        // Проверяем, нет ли препятствий на пути луча
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, direction, out hit, distance, detectionLayer))
        {
            return hit.collider.transform == detectedPlayer;
        }

        return false;
    }

    private void PlayerFullyDetected()
    {
        //audioAlarm.Pause();
        Debug.Log("Игрок полностью обнаружен!");
        odjectText.SetActive(false);
        activeTimer = false;
        ResetDetection();
        Time.timeScale = 0;
        // Здесь можно вызвать события (например, тревогу)
    }

    private void ResetDetection()
    {
        //audioAlarm.Pause();
        odjectText.SetActive(false);
        currentDetectionTime = maxDetectionTime;
        isDetecting = false;
        detectedPlayer = null;
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

