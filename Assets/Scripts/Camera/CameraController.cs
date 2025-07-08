using System.Collections.Generic;
using UnityEngine;

public class CameraController : Subscriber
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] cameras;
    [SerializeField] private bool startDisable = true;

    private readonly Dictionary<GameObject,bool> _camerasInUse = new();
    private bool _disabled;

    public void EnableCamera(GameObject cam)
    {
        _camerasInUse[cam] = true;
        cam.SetActive(true);
    }

    public void DisableCamera(GameObject cam)
    {
        _camerasInUse[cam] = false;
        cam.SetActive(false);
    }

    private void Start()
    {
        if (startDisable)
        {
            foreach (var cam in cameras)
            {
                cam.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        
        foreach (var cam in cameras)
        {
            if(_camerasInUse.TryGetValue(cam, out var value))
                if(!value)
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
