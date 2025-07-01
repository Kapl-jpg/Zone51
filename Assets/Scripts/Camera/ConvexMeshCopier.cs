using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class CombineConvexColliders : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsWithColliders; // Объекты с MeshCollider
    [SerializeField] private float vertexReductionRatio = 0.5f; // Степень сокращения вершин (0-1)

    [ContextMenu("Combine and Generate Convex Collider")]
    private void CombineAndGenerateConvexCollider()
    {
        // Проверка наличия MeshFilter
        MeshFilter targetMeshFilter = GetComponent<MeshFilter>();
        if (targetMeshFilter == null)
        {
            Debug.LogError("MeshFilter отсутствует на целевом объекте " + gameObject.name);
            return;
        }

        // Собираем MeshCollider и их меши
        var meshColliders = objectsWithColliders
            .Select(obj => obj.GetComponent<MeshCollider>())
            .Where(mc => mc != null && mc.sharedMesh != null)
            .ToArray();

        if (meshColliders.Length == 0)
        {
            Debug.LogError("Нет объектов с валидными MeshCollider или меши отсутствуют!");
            return;
        }

        // Логируем информацию об исходных меша
        foreach (var mc in meshColliders)
        {
            Debug.Log($"Меш из {mc.gameObject.name}: {mc.sharedMesh.vertexCount} вершин, {mc.sharedMesh.triangles.Length / 3} треугольников");
        }

        // Объединяем меши
        CombineInstance[] combine = new CombineInstance[meshColliders.Length];
        for (int i = 0; i < meshColliders.Length; i++)
        {
            combine[i].mesh = meshColliders[i].sharedMesh;
            combine[i].transform = meshColliders[i].transform.localToWorldMatrix;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine, true, true);
        Debug.Log($"Объединенный меш: {combinedMesh.vertexCount} вершин, {combinedMesh.triangles.Length / 3} треугольников");

        // Упрощаем меш
        Mesh simplifiedMesh = SimplifyMesh(combinedMesh, vertexReductionRatio);
        if (simplifiedMesh.vertexCount == 0 || simplifiedMesh.triangles.Length == 0)
        {
            Debug.LogError("Упрощенный меш пуст! Попробуйте уменьшить vertexReductionRatio или проверьте исходные меши.");
            return;
        }

        // Применяем упрощенный меш к MeshCollider
        MeshCollider targetCollider = GetComponent<MeshCollider>();
        if (targetCollider == null)
        {
            targetCollider = gameObject.AddComponent<MeshCollider>();
        }

        targetCollider.sharedMesh = simplifiedMesh;
        targetCollider.convex = true;
        targetCollider.cookingOptions = MeshColliderCookingOptions.CookForFasterSimulation |
                                       MeshColliderCookingOptions.EnableMeshCleaning |
                                       MeshColliderCookingOptions.WeldColocatedVertices;

        // Сохраняем объединенный (не упрощенный) меш в MeshFilter для визуализации
        targetMeshFilter.sharedMesh = combinedMesh;

        Debug.Log($"Итоговый convex коллайдер: {simplifiedMesh.vertexCount} вершин, {simplifiedMesh.triangles.Length / 3} треугольников");

        // Отключаем исходные объекты (опционально)
        foreach (GameObject obj in objectsWithColliders)
        {
            obj.SetActive(false);
        }
    }

    private Mesh SimplifyMesh(Mesh mesh, float ratio)
    {
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        if (vertices.Length == 0 || triangles.Length == 0)
        {
            Debug.LogError("Исходный меш пуст: вершины или треугольники отсутствуют!");
            return mesh;
        }

        // Ограничиваем сокращение вершин
        int targetVertexCount = Mathf.Max(4, (int)(vertices.Length * (1f - ratio)));
        if (vertices.Length <= targetVertexCount)
        {
            Debug.Log("Упрощение не требуется, возвращаем исходный меш.");
            return mesh;
        }

        // Уникальные вершины
        List<Vector3> newVertices = new List<Vector3>();
        int[] vertexMap = new int[vertices.Length];
        float mergeDistance = 0.01f; // Минимальное расстояние для слияния вершин

        for (int i = 0; i < vertices.Length && newVertices.Count < targetVertexCount; i++)
        {
            bool isUnique = true;
            for (int j = 0; j < newVertices.Count; j++)
            {
                if (Vector3.Distance(vertices[i], newVertices[j]) < mergeDistance)
                {
                    vertexMap[i] = j;
                    isUnique = false;
                    break;
                }
            }
            if (isUnique)
            {
                newVertices.Add(vertices[i]);
                vertexMap[i] = newVertices.Count - 1;
            }
        }

        if (newVertices.Count < 3)
        {
            Debug.LogError("Слишком мало вершин после упрощения!");
            return mesh;
        }

        // Перестраиваем треугольники
        List<int> newTrianglesList = new List<int>();
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int v0 = vertexMap[triangles[i]];
            int v1 = vertexMap[triangles[i + 1]];
            int v2 = vertexMap[triangles[i + 2]];

            // Проверяем, что треугольник не вырожденный
            if (v0 != v1 && v1 != v2 && v2 != v0)
            {
                newTrianglesList.Add(v0);
                newTrianglesList.Add(v1);
                newTrianglesList.Add(v2);
            }
        }

        if (newTrianglesList.Count < 3)
        {
            Debug.LogError("После упрощения не осталось валидных треугольников!");
            return mesh;
        }

        // Создаем новый меш
        Mesh simplifiedMesh = new Mesh
        {
            vertices = newVertices.ToArray(),
            triangles = newTrianglesList.ToArray()
        };

        simplifiedMesh.RecalculateNormals();
        simplifiedMesh.RecalculateBounds();

        // Проверка на лимит треугольников
        if (simplifiedMesh.triangles.Length / 3 > 255)
        {
            Debug.LogWarning($"Упрощенный меш имеет {simplifiedMesh.triangles.Length / 3} треугольников, превышает лимит 255 для convex коллайдера!");
        }

        return simplifiedMesh;
    }
}