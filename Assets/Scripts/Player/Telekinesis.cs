using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Telekinesis : MonoBehaviour
{
    [SerializeField] private InputMeneger inputMeneger;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float grabForce = 10;
    [SerializeField] private float maxDistance = 10;
    [SerializeField] private string grabbableTag = "ForTelekinesis";
    [SerializeField] private float maxChargeTime = 10;
    [SerializeField] private float maxThrowForce = 25;

    private Rigidbody grabbedRigidbody;
    private Renderer objectRenderer;

    private bool isGrabbing = false;
    private bool activeCharge = false;
    private float chargeTime = 0f;

    private void FixedUpdate()
    {
        if (inputMeneger.InputMouseLeftButton())
        {
            if (isGrabbing == false)
            {
                GrabObject();
            }
            else
            {
                ReleaseObject();
            }
        }

        if (inputMeneger.InputMouseRightButton() && isGrabbing)
        {
            activeCharge = true;
            ChargeThrow();
        }

        if (activeCharge && inputMeneger.InputMouseRightButton() == false && isGrabbing)
        {
            ThrowObject();
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

                    //SettingTransparencyObjiect(0.5f);
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
            chargeTime = 0f;

        }
    }

    private void PullObject()
    {
        if (grabbedRigidbody == null) return;
        
        float smoothSpeed = 5f;
        Vector3 targetPosition = grabPoint.position;
        Vector3 newPosition = Vector3.Lerp(grabbedRigidbody.position, targetPosition, smoothSpeed * Time.deltaTime);
        grabbedRigidbody.MovePosition(newPosition);
    }

    private void ChargeThrow()
    {
        chargeTime += Time.deltaTime;

        if (chargeTime > maxChargeTime)
        {
            ThrowObject();
        }
        print(chargeTime); // Temporarily
    }

    private void ThrowObject()
    {
        if (grabbedRigidbody != null)
        {
            float throwForce = Mathf.Clamp01(chargeTime / maxChargeTime) * maxThrowForce;
            Vector3 throwDirection = Camera.main.transform.forward;
            grabbedRigidbody.AddForce(throwDirection * throwForce, ForceMode.Impulse);
            ReleaseObject();
            activeCharge = false;
        }
    }

    private void SettingTransparencyObjiect(float transparency)
    {
        objectRenderer = grabbedRigidbody.GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            Color objectColor = objectRenderer.material.color;
            objectColor.a = transparency;
            objectRenderer.material.color = objectColor;
        }
    }
}
