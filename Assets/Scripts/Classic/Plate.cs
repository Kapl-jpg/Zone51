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
    
    private Material _plateMaterial;
    private bool _canUse;
    
    private void Start()
    {
        _canUse = active;
        _plateMaterial = new Material(meshRenderer.material);
        meshRenderer.material = _plateMaterial;
        _plateMaterial.SetFloat("_Enable", active ? 1f : 0f);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(!_canUse) return;
        
        if (!other.gameObject.CompareTag("Player")) return;
        
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");

        if (!final)
        {
            if (plateType == CharacterType.Neutral || characterType == plateType)
            {
                if (nextPlate)
                {
                    plateAnimation.Enable();
                    nextPlate._canUse = true;
                    EnablePlate();
                }

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

    [Event("ResetPlate")]
    private void OnResetPlate()
    {
        _canUse = active;
        plateAnimation.Disable();
        _plateMaterial.SetFloat("_Enable", active? 1f : 0f);
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
    }
}
