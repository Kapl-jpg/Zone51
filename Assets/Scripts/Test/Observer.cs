using Enums;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 5;
    [SerializeField] private int distance = 10;
    [SerializeField] private float horizontalAngle = 90f;
    [SerializeField] private float verticalAngle = 60f;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        if (RayToScan())
        {
            var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
            if (characterType == CharacterType.Alien)
            {
                print("GameOver");
            }
        }
    }

    private bool RayToScan()
    {
        bool found = false;

        float vertStep = verticalAngle / Mathf.Max(rows - 1, 1);
        float horStep = horizontalAngle / Mathf.Max(columns - 1, 1);

        for (int y = 0; y < rows; y++)
        {
            float vert = -verticalAngle / 2 + y * vertStep;

            for (int x = 0; x < columns; x++)
            {
                float hor = -horizontalAngle / 2 + x * horStep;

                Quaternion rotation = Quaternion.Euler(vert, hor, 0);
                Vector3 dir = transform.TransformDirection(rotation * Vector3.forward);

                if (GetRaycast(dir))
                {
                    found = true;
                }
            }
        }

        return found;
    }

    private bool GetRaycast(Vector3 dir)
    {
        Vector3 origin = transform.position + offset;
        if (Physics.Raycast(origin, dir, out RaycastHit hit, distance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.DrawLine(origin, hit.point, Color.green);
                return true;
            }
            else
            {
                Debug.DrawLine(origin, hit.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(origin, dir * distance, Color.red);
        }

        return false;
    }
}