using Classic;
using Enums;
using UnityEngine;

public class Plate : Subscriber
{
    [SerializeField] private Plate nextPlate;
    [SerializeField] private PlateAnimation plateAnimation;
    [SerializeField] private CharacterType plateType;
    [SerializeField] private bool active;
    [SerializeField] private bool final;
    
    private bool _canUse;
    
    private void Start()
    {
        _canUse = active;
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
                }

                _canUse = false;
            }
            else
            {
                print(gameObject.name);
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
    }

    private void FinalEvent()
    {
        EventManager.Publish("OpenHangarDoor");
    }
}
