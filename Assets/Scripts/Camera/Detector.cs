using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Detector : MonoBehaviour
{
    public Transform targetTransform;

    public float detectionDistance = 15f;

    public float sphereRadius = 0.5f; 

    public LayerMask detectionLayerMask;

    void Start()
    {
        if (targetTransform == null)
        {
            Debug.LogError("Целевой персонаж не назначен! Пожалуйста, перетащите Transform персонажа в поле Inspector.", this);
            enabled = false; 
            return;
        }

    }

    void Update()
    {

        if (targetTransform == null)
        {
            return;
        }

        Vector3 cameraEyePosition = this.transform.position;

        float distanceToTarget = Vector3.Distance(cameraEyePosition, targetTransform.position);

        if (distanceToTarget > detectionDistance)
        {
            return;
        }

        Vector3 directionToTarget = (targetTransform.position - cameraEyePosition).normalized;

        RaycastHit hit;
        if (Physics.SphereCast(cameraEyePosition, sphereRadius, directionToTarget, out hit, detectionDistance, detectionLayerMask))
        {

            if (hit.collider.transform == targetTransform)
            {
                //if (Physics.Raycast())
            }
        }
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying && targetTransform != null)
        {
            Gizmos.color = Color.red;

            Vector3 cameraEyePosition = this.transform.position;
            Vector3 directionToTarget = (targetTransform.position - cameraEyePosition).normalized;

            Gizmos.DrawWireSphere(cameraEyePosition, sphereRadius);

            Gizmos.DrawLine(cameraEyePosition, cameraEyePosition + directionToTarget * detectionDistance);

            Gizmos.DrawWireSphere(cameraEyePosition + directionToTarget * detectionDistance, sphereRadius);
        }
    }
}

