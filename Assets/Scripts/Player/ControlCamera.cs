using Unity.Cinemachine;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    public Camera mainCamera; // Ссылка на вашу основную камеру
    public float offsetDistance = 1f; // Расстояние от камеры до препятствия
    public float checkInterval = 0.1f; // Частота проверок
    private float nextCheckTime;

    void Update()
    {
        if (Time.time > nextCheckTime)
        {
            nextCheckTime = Time.time + checkInterval;
            CheckCollision();
        }
    }

    void CheckCollision()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        Collider[] colliders = Physics.OverlapSphere(cameraPosition, offsetDistance);

        foreach (Collider collider in colliders)
        {
            if (collider != null && collider.gameObject != mainCamera.gameObject) //Исключаем коллизию с самой камерой
            {
                Vector3 directionToCollider = (collider.transform.position - cameraPosition).normalized;
                float distanceToCollider = Vector3.Distance(cameraPosition, collider.transform.position);

                // Проверяем, находится ли камера внутри коллайдера.
                if (distanceToCollider < offsetDistance)
                {
                    Vector3 newPosition = cameraPosition - directionToCollider * (offsetDistance - distanceToCollider);
                    mainCamera.transform.position = newPosition;
                }
            }
        }
    }
}
