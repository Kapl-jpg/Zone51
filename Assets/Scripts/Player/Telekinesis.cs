using Enums;
using Interfaces;
using Unity.Cinemachine;
using UnityEngine;

public class Telekinesis : Subscriber
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private CinemachineCamera thirdPersonCamera;
    [SerializeField] private AudioSource audioLifting;
    [SerializeField] private AudioSource audioThrowing;
    [SerializeField] private AudioSource audioRetention;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float maxDistance = 10;
    [SerializeField] private float maxChargeTime = 5;
    [SerializeField] private float maxHoldTime = 10;
    [SerializeField] private float maxThrowForce = 25;
    [SerializeField] private float turnSmoothness = 5f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool needGetAbility = true;
    private const float wallEpsilon = 0.02f;
    private const float slideThreshold = 0.01f;
    private ITelekinesisVisible _telekinesisVisible;
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
        DetectObject();
        var chipDisabled = RequestManager.GetValue<bool>("ChipDisable");
        if(!chipDisabled && needGetAbility) return;
        
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
        if (characterType == CharacterType.Alien)
        {
            if (inputManager.InputMouseLeftButton())
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
        
        if (inputManager.InputMouseRightButton() && isGrabbing)
        {
            activeCharge = true;
            ChargeThrow();
        }

        if (activeCharge && inputManager.InputMouseRightButton() == false && isGrabbing)
        {
            ThrowObject();
        }

        Debug.DrawRay(UnityEngine.Camera.main.transform.position, UnityEngine.Camera.main.transform.forward * maxDistance, Color.red, 1f);
    }

    [Event("ReleaseTelekinesis")]
    private void ReleaseTelekinesis()
    {
        ReleaseObject();
    }
    
    private void GrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(UnityEngine.Camera.main.transform.position, UnityEngine.Camera.main.transform.forward, out hit, maxDistance, layerMask))
        {
            hit.collider.TryGetComponent(out ObjectForTelekinesis objectForTelekinesis);
            hit.collider.TryGetComponent(out IInteractable interactable);
            
            if (objectForTelekinesis != null)
            {
                grabbedRigidbody = hit.rigidbody;
                if (grabbedRigidbody != null)
                {
                    audioLifting.Play();
                    
                    interactable?.Interact();
                    
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

    private void DetectObject()
    {
        if (!isGrabbing)
        {
            RaycastHit hit;
            var chipDisabled = RequestManager.GetValue<bool>("ChipDisable");
            var characterType = RequestManager.GetValue<CharacterType>("CharacterType");

            if (!chipDisabled && needGetAbility) return;

            if (Physics.Raycast(UnityEngine.Camera.main.transform.position, UnityEngine.Camera.main.transform.forward,
                    out hit, maxDistance, layerMask))
            {
                if (hit.collider.TryGetComponent(out ITelekinesisVisible telekinesisVisible))
                {
                    if (characterType == CharacterType.Alien)
                    {
                        if (_telekinesisVisible == null)
                        {
                            _telekinesisVisible = telekinesisVisible;
                            _telekinesisVisible.Show();
                            EventManager.Publish("ShowTip", TipType.Telekinesis);
                        }
                        else
                        {
                            if (_telekinesisVisible != telekinesisVisible)
                            {
                                _telekinesisVisible?.Hide();
                                EventManager.Publish("ShowTip", TipType.Telekinesis);
                                _telekinesisVisible = telekinesisVisible;
                                _telekinesisVisible.Show();
                            }
                        }
                    }
                    else
                    {
                        if (_telekinesisVisible != null)
                        {
                            EventManager.Publish("HideTip");
                            _telekinesisVisible.Hide();
                            _telekinesisVisible = null;
                        }
                    }
                }
                else
                {
                    if (_telekinesisVisible != null)
                    {
                        EventManager.Publish("HideTip");
                        _telekinesisVisible.Hide();
                        _telekinesisVisible = null;
                    }
                }
            }
            else
            {
                if (_telekinesisVisible != null)
                {
                    EventManager.Publish("HideTip");
                    _telekinesisVisible.Hide();
                    _telekinesisVisible = null;
                }
            }
        }
        else
        {
            if (!grabbedRigidbody) return;
            
            if (grabbedRigidbody.gameObject.TryGetComponent(out ITelekinesisVisible telekinesisVisible))
            {
                telekinesisVisible?.Show();
            }
                
            EventManager.Publish("ShowTip", TipType.TelekinesisDrop);
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

            audioRetention.Stop();
            if (!audioRetention.isPlaying)
            {
                audioThrowing.Play();
            }
        }
    }

    private void PullObject()
        {
            if (grabbedRigidbody == null) return;
            
            Vector3 currentPos = grabbedRigidbody.position;
            Vector3 targetPos  = grabPoint.position;
    
            Vector3 dir  = (targetPos - currentPos);
            float   dist = dir.magnitude;
            if (dist < 0.001f) return;
    
            dir /= dist;
            float maxStep = smoothSpeed * Time.fixedDeltaTime;
            float step    = Mathf.Min(maxStep, dist);
    
            if (grabbedRigidbody.SweepTest(dir, out RaycastHit hit, step))
            {
                float allowedMove = hit.distance;
    
                Vector3 posToWall = currentPos + dir * allowedMove;
                
                Vector3 slideDir = Vector3.ProjectOnPlane(dir, hit.normal).normalized;
    
                float slideStep = step - allowedMove;
                Vector3 finalPos = posToWall + slideDir * slideStep;
    
                grabbedRigidbody.MovePosition(finalPos);
            }
            else
            {
                Vector3 finalPos = currentPos + dir * step;
                grabbedRigidbody.MovePosition(finalPos);
            }
    
            if (!audioRetention.isPlaying && !audioLifting.isPlaying)
            {
                bool activeAudio = true;
                if (activeAudio)
                {
                    audioRetention.Play();
                    activeAudio = false;
                    print("play");
                }
    
                if (!audioThrowing.isPlaying)
                {
                    activeAudio = true;
                }
            }
    
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
    }

    private void ThrowObject()
    {
        if (grabbedRigidbody != null)
        {
            float throwForce = Mathf.Clamp01(chargeTime / maxChargeTime) * maxThrowForce;
            Vector3 throwDirection = UnityEngine.Camera.main.transform.forward;
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
        Gizmos.DrawRay(UnityEngine.Camera.main.transform.position, UnityEngine.Camera.main.transform.forward * maxDistance);
    }
}