using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class VisionCone : MonoBehaviour
{
    public float distance = 10f;
    public float horizontalAngle = 90f;
    public float verticalAngle = 60f;
    public Material visionMaterial;
    public Vector3 offset;

    [ContextMenu("Generate")]
    private void Create()
    {
        MeshFilter filter = gameObject.GetComponent<MeshFilter>();
        if (filter == null) filter = gameObject.AddComponent<MeshFilter>();

        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        if (renderer == null) renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.material = visionMaterial;

        Mesh mesh = GenerateVisionCone();
        filter.sharedMesh = mesh;

        SaveMeshAsset(mesh, "VisionConeMesh");
    }

    private Mesh GenerateVisionCone()
    {
        Mesh mesh = new Mesh { name = "VisionConeMesh" };

        // Используем offset как начало конуса
        Vector3 origin = offset;

        // Рассчитываем вершины с учетом углов, как в Observer
        Vector3 topLeft = offset + Quaternion.Euler(-verticalAngle * 0.5f, -horizontalAngle * 0.5f, 0) * Vector3.forward * distance;
        Vector3 topRight = offset + Quaternion.Euler(-verticalAngle * 0.5f, horizontalAngle * 0.5f, 0) * Vector3.forward * distance;
        Vector3 bottomLeft = offset + Quaternion.Euler(verticalAngle * 0.5f, -horizontalAngle * 0.5f, 0) * Vector3.forward * distance;
        Vector3 bottomRight = offset + Quaternion.Euler(verticalAngle * 0.5f, horizontalAngle * 0.5f, 0) * Vector3.forward * distance;

        mesh.vertices = new Vector3[]
        {
            origin,     // 0
            topLeft,    // 1
            topRight,   // 2
            bottomRight,// 3
            bottomLeft  // 4
        };

        mesh.triangles = new int[]
        {
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            0, 4, 1
        };

        mesh.RecalculateNormals();
        return mesh;
    }

#if UNITY_EDITOR
    private void SaveMeshAsset(Mesh mesh, string name)
    {
        string path = $"Assets/{name}.asset";
        AssetDatabase.CreateAsset(mesh, AssetDatabase.GenerateUniqueAssetPath(path));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Mesh saved to: {path}");
    }
#endif
    
    private void OnDrawGizmos()
    {
        Vector3 topLeft = offset + Quaternion.Euler(-verticalAngle * 0.5f, -horizontalAngle * 0.5f, 0) * Vector3.forward * distance;
        Vector3 topRight = offset + Quaternion.Euler(-verticalAngle * 0.5f, horizontalAngle * 0.5f, 0) * Vector3.forward * distance;
        Vector3 bottomLeft = offset + Quaternion.Euler(verticalAngle * 0.5f, -horizontalAngle * 0.5f, 0) * Vector3.forward * distance;
        Vector3 bottomRight = offset + Quaternion.Euler(verticalAngle * 0.5f, horizontalAngle * 0.5f, 0) * Vector3.forward * distance;

        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawLine(offset, topLeft);
        Gizmos.DrawLine(offset, topRight);
        Gizmos.DrawLine(offset, bottomLeft);
        Gizmos.DrawLine(offset, bottomRight);
    }
}