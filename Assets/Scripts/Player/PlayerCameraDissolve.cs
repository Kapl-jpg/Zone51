using UnityEngine;

namespace Player
{
    public class PlayerCameraDissolve: MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Vector2 borders;
        [SerializeField] private SkinnedMeshRenderer alienMeshRenderer;
        [SerializeField] private SkinnedMeshRenderer humanMeshRenderer;
        
        private void Update()
        {
            var distance = Vector3.Distance(cameraTransform.position, transform.position);
            var t = Mathf.InverseLerp(borders.x,borders.y,Mathf.Clamp(distance, borders.x, borders.y));
            
            if (alienMeshRenderer.gameObject.activeInHierarchy)
            {
                alienMeshRenderer.material.SetFloat("_CameraDissolveValue", t);
            }
            else
            {
                alienMeshRenderer.material.SetFloat("_CameraDissolveValue", 1);
            }

            if (humanMeshRenderer.gameObject.activeInHierarchy)
            {
                humanMeshRenderer.material.SetFloat("_CameraDissolveValue", t);
            }
        }
    }
}