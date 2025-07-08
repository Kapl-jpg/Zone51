using System.Collections.Generic;
using UnityEngine;

public class CameraController : Subscriber
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] cameras;
    [SerializeField] private float maxDistance;

    private readonly Dictionary<GameObject,bool> _camerasInUse = new();
    private bool _disabled;

    [Event("AddActiveCamera")]
    private void AddActiveCamera(GameObject cam)
    {
        _camerasInUse[cam] = true;
    }

    [Event("RemoveActiveCamera")]
    private void RemoveActiveCamera(GameObject cam)
    {
        _camerasInUse[cam] = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        
        foreach (var cam in cameras)
        {
            if(_camerasInUse.ContainsKey(cam))
                if(!_camerasInUse[cam])
                    continue;
            
            cam.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        
        foreach (var cam in cameras)
        {
            cam.SetActive(false);
        }
    }
}
