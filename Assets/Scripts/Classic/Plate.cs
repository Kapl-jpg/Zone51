using System;
using Classic;
using Enums;
using UnityEngine;

public class Plate : Subscriber
{
    [SerializeField] private Plate nextPlate;
    [SerializeField] private PlateAnimation plateAnimation;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private CharacterType plateType;
    [SerializeField] private bool active;
    [SerializeField] private bool final;

    private GameObject _lastPlate;
    private AudioSource _audioClassic;
    private Material _plateMaterial;
    private bool _active;
    private bool _canUse;
    private bool _lockPlate;
    
    private void Start()
    {
        _audioClassic = GetComponent<AudioSource>();
        _canUse = active;
        _plateMaterial = new Material(meshRenderer.material);
        meshRenderer.material = _plateMaterial;
        _plateMaterial.SetFloat("_Enable", active ? 1f : 0f);
    }

    private void Update()
    {
        if(_lockPlate) return;
        
        if(_canUse) return;
        
        if (transform.childCount > 0)
        {
            var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
            if (plateType != CharacterType.Neutral && characterType != plateType)
            {
                EventManager.Publish("ResetPlate");
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_canUse)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            var characterType = RequestManager.GetValue<CharacterType>("CharacterType");

            if (!final)
            {
                if (plateType == CharacterType.Neutral || characterType == plateType)
                {
                    EventManager.Publish("LastPlate", gameObject);
                    if (nextPlate)
                    {
                        _audioClassic.Play();
                        plateAnimation.Enable();
                        nextPlate._canUse = true;
                        EnablePlate();
                    }

                    _active = true;
                    _canUse = false;
                }
                else
                {
                    EventManager.Publish("ResetPlate");
                }
            }
            else
            {
                FinalEvent();
            }
        }
        else
        {
            if(gameObject != _lastPlate)
                EventManager.Publish("ResetPlate");
        }
    }

    [Event("LastPlate")]
    private void LastPlate(GameObject lastPlate)
    {
        _lastPlate = lastPlate;
    }
    
    [Event("ResetPlate")]
    private void OnResetPlate()
    {
        if (_lockPlate) return;
        
        if (_active)
        {
            _audioClassic.Play();
            plateAnimation.Disable();
            _active = false;
        }

        _plateMaterial.SetFloat("_Enable", active ? 1f : 0f);
        _canUse = active;
    }

    [Event("LockPlate")]
    private void OnLockPlate()
    {
        _lockPlate = true;
    }
    
    private void EnablePlate()
    {
        _plateMaterial.SetFloat("_Enable", 1f);
    }

    private void FinalEvent()
    {
        plateAnimation.Enable();
        EnablePlate();
        _canUse = false;
        EventManager.Publish("OpenTopDoor");
        EventManager.Publish("LockPlate");
    }
}
