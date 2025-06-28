using UnityEngine;
using System.Collections.Generic;

public class VisionLineSpawner : MonoBehaviour
{
    [Header("Область обзора")]
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 5;
    [SerializeField] private float distance = 10f;
    [SerializeField] private float horizontalAngle = 90f;
    [SerializeField] private float verticalAngle = 60f;
    [SerializeField] private Vector3 offset;

    [Header("LineRenderer")]
    [SerializeField] private Material lineMaterial;
    [SerializeField] private float lineWidth = 0.02f;
    [SerializeField] [Range(0f, 1f)] private float spawnChance = 0.3f;

    private List<LineRenderer> lines = new List<LineRenderer>();
    private List<Vector2> activeDirections = new List<Vector2>();

    private void Start()
    {
        GenerateLines();
    }

    private void Update()
    {
        UpdateLinePositions();
    }
    
    [ContextMenu("Generate Lines")]
    private void GenerateLines()
    {
        // Очищаем существующие лучи
        foreach (var line in lines)
        {
            if (line != null) Destroy(line.gameObject);
        }
        lines.Clear();
        activeDirections.Clear();

        float horStep = columns > 1 ? horizontalAngle / (columns - 1) : 0f;
        float verStep = rows > 1 ? verticalAngle / (rows - 1) : 0f;

        for (int y = 0; y < rows; y++)
        {
            float ver = -verticalAngle / 2f + y * verStep;

            for (int x = 0; x < columns; x++)
            {
                if (Random.value > spawnChance) continue;

                float hor = -horizontalAngle / 2f + x * horStep;

                // Создаем новый объект для луча
                GameObject lineObj = new GameObject($"VisionLine_{x}_{y}");
                lineObj.transform.SetParent(transform, false); // Привязываем к родителю
                lineObj.transform.localPosition = Vector3.zero; // Сбрасываем локальную позицию

                LineRenderer lr = lineObj.AddComponent<LineRenderer>();
                lr.material = lineMaterial;
                lr.startWidth = lineWidth;
                lr.endWidth = lineWidth;
                lr.positionCount = 2;
                lr.useWorldSpace = true; // Мировые координаты
                lr.startColor = Color.yellow;
                lr.endColor = Color.red;

                lines.Add(lr);
                activeDirections.Add(new Vector2(hor, ver));
            }
        }
    }

    private void UpdateLinePositions()
    {
        // Начальная позиция: позиция родителя + смещение в мировых координатах
        Vector3 origin = transform.position + transform.TransformVector(offset);

        for (int i = 0; i < lines.Count; i++)
        {
            float hor = activeDirections[i].x;
            float ver = activeDirections[i].y;

            // Поворот: сначала по горизонтали, затем по вертикали
            Quaternion rotation = Quaternion.Euler(0f, hor, 0f) * Quaternion.Euler(ver, 0f, 0f);
            Vector3 localDir = rotation * Vector3.forward;
            Vector3 worldDir = transform.TransformDirection(localDir).normalized;

            Vector3 end = origin + worldDir * distance;

            lines[i].SetPosition(0, origin); // Начало луча
            lines[i].SetPosition(1, end);   // Конец луча

            // Отладка направления
            Debug.DrawRay(origin, worldDir * distance, Color.green, 0.1f);
        }
    }
}