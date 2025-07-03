using System;
using Enums;
using Unity.Cinemachine;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    [SerializeField] private InputMeneger inputMeneger;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private CinemachineCamera thirdPersonCamera;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float grabForce = 10;
    [SerializeField] private float maxDistance = 10;
    [SerializeField] private float maxChargeTime = 5;
    [SerializeField] private float maxHoldTime = 10;
    [SerializeField] private float maxThrowForce = 25;
    [SerializeField] private float turnSmoothness = 5f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool needGetAbility = true;
    private Rigidbody grabbedRigidbody;

    private bool isGrabbing = false;
    private bool activeCharge = false;
    private float chargeTime = 0f;
    private float _holdTime;

    private void FixedUpdate()
    {
        if (isGrabbing)
        {
            PullObject();
        }
    }

    private void Update()
    {
        var chipDisabled = RequestManager.GetValue<bool>("ChipDisable");
        if(!chipDisabled && needGetAbility) return;
        
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
        if (inputMeneger.InputMouseLeftButton())
        {
            if (characterType == CharacterType.Alien)
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
        }

        if (isGrabbing)
        {
            if(characterType == CharacterType.Human)
                ReleaseObject();
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

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxDistance, Color.red, 1f);
    }

    private void GrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, layerMask))
        {
            ObjectForTelekinesis objectForTelekinesis = hit.collider.GetComponent<ObjectForTelekinesis>();

            if (objectForTelekinesis != null)
            {
                grabbedRigidbody = hit.rigidbody;
                if (grabbedRigidbody != null)
                {
                    isGrabbing = true;
                    grabbedRigidbody.freezeRotation = true;
                    grabbedRigidbody.useGravity = false;

                    objectForTelekinesis.PurposeRealMass();
                    objectForTelekinesis.SettingTransparent(false);
                    objectForTelekinesis.ActivatorCheckingGravity(true);
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
            _holdTime = 0f;
        }
    }

    private void PullObject()
    {
        if (grabbedRigidbody == null) return;
        
        Vector3 targetPosition = grabPoint.position;
        Vector3 newPosition = Vector3.Lerp(grabbedRigidbody.position, targetPosition, smoothSpeed * Time.deltaTime);
        grabbedRigidbody.MovePosition(newPosition);

        ObjectMonitoring();
    }

    private void ChargeThrow()
    {
        chargeTime = Mathf.Clamp(chargeTime + Time.deltaTime, 0, maxChargeTime);
        _holdTime += Time.deltaTime;
        
        if (_holdTime > maxHoldTime)
        {
            ReleaseObject();
        }
        //print(chargeTime); // Temporarily
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

    private void ObjectMonitoring()
    {
        if (!ActiveFirstPersonCamera())
        {
            Vector3 lookDirection = grabbedRigidbody.position - transform.position;
            lookDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * turnSmoothness);
        }
        
    }

    private bool ActiveFirstPersonCamera()
    {
        return RequestManager.GetValue<bool>("ActivateFirstPersonCamera");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxDistance);
    }
}
