using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    [SerializeField] private InputMeneger inputMeneger;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private  float grabForce = 10f;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private string grabbableTag = "ForTelekinesis";
    

    private Rigidbody grabbedRigidbody;
    private bool isGrabbing = false;

    private void Update()
    {
        if (inputMeneger.InputMouseLeftButton())
        {
            GrabObject();
        }

        if (inputMeneger.InputMouseRightButton())
        {
            ReleaseObject();
        }

        if (isGrabbing)
        {
            PullObject();
        }
    }

    private void GrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
            if (hit.collider.CompareTag(grabbableTag))
            {
                grabbedRigidbody = hit.rigidbody;
                if (grabbedRigidbody != null)
                {
                    isGrabbing = true;
                    grabbedRigidbody.useGravity = false;
                    grabbedRigidbody.freezeRotation = true;
                }
            }
        }
    }

    private void ReleaseObject()
    {
        if (isGrabbing)
        {
            isGrabbing = false;
            grabbedRigidbody.useGravity = true;
            grabbedRigidbody.freezeRotation = false;
            grabbedRigidbody = null;
        }
    }

    private void PullObject()
    {
        if (grabbedRigidbody != null)
        {
            float smoothSpeed = 5f; 
            Vector3 targetPosition = grabPoint.position;
            Vector3 newPosition = Vector3.Lerp(grabbedRigidbody.position, targetPosition, smoothSpeed * Time.deltaTime);
            grabbedRigidbody.MovePosition(newPosition);
        }
    }
}
