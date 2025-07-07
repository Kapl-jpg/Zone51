using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] cameras;
    [SerializeField] private float maxDistance;

    private bool _disabled;
    
    private void Update()
    {
        var distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance > maxDistance)
        {
            if (_disabled) return;
            
            foreach (var cam in cameras)
            {
                cam.SetActive(false);
            }
                
            _disabled = true;
        }
        else
        {
            if (!_disabled) return;
            
            foreach (var cam in cameras)
            {
                cam.SetActive(true);
            }
                
            _disabled = false;
        }
    }
}
