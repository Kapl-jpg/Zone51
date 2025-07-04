using Enums;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private Plate nextPlate;
    [SerializeField] private CharacterType plateType;
    [SerializeField] private Animator animator;
    [SerializeField] private bool active;
    [SerializeField] private bool final;
    
    private bool _activated;
    private bool _canUse;
    
    private void Start()
    {
        _canUse = active;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(!_canUse || _activated) return;
        
        if (!other.gameObject.CompareTag("Player")) return;
        
        var characterType = RequestManager.GetValue<CharacterType>("CharacterType");
        
        if (characterType != plateType) return;

        if(nextPlate)
            nextPlate._canUse = true;
        
        _canUse = false;
        print(plateType);
    }

    [Event("ResetPlate")]
    private void OnResetPlate()
    {
        _canUse = active;
        _activated = false;
    }

    private void FinalEvent()
    {
        
    }
}
