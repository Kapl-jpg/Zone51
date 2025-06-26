using Unity.Cinemachine;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    public Camera mainCamera; // ������ �� ���� �������� ������
    public float offsetDistance = 1f; // ���������� �� ������ �� �����������
    public float checkInterval = 0.1f; // ������� ��������
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
            if (collider != null && collider.gameObject != mainCamera.gameObject) //��������� �������� � ����� �������
            {
                Vector3 directionToCollider = (collider.transform.position - cameraPosition).normalized;
                float distanceToCollider = Vector3.Distance(cameraPosition, collider.transform.position);

                // ���������, ��������� �� ������ ������ ����������.
                if (distanceToCollider < offsetDistance)
                {
                    Vector3 newPosition = cameraPosition - directionToCollider * (offsetDistance - distanceToCollider);
                    mainCamera.transform.position = newPosition;
                }
            }
        }
    }
}
