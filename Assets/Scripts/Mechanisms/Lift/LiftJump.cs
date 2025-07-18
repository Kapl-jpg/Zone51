using UnityEngine;

public class LiftJump : Subscriber
{
    [SerializeField] private GameObject liftLamp;
    [SerializeField] private Material liftLampMaterial;
    
    [Event("EnableLift")]
    private void EnableLift()
    {
        liftLamp.SetActive(true);
        liftLampMaterial.SetFloat("_Enable", 1f);
    }
    
    [Event("DisableLift")]
    private void DisableLift()
    {
        liftLamp.SetActive(false);
        liftLampMaterial.SetFloat("_Enable", 0f);
    }
}
