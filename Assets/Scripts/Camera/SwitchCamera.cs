using Unity.Cinemachine;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] CinemachineCamera thirdPersonCamera;
    [SerializeField] CinemachineCamera firstPersonCamera;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (thirdPersonCamera.Priority > firstPersonCamera.Priority)
            {
                firstPersonCamera.Priority = thirdPersonCamera.Priority;
            }
            else
            {
                thirdPersonCamera.Priority = firstPersonCamera.Priority;
            }
        }
    }
}
