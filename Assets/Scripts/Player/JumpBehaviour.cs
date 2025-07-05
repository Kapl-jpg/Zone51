using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    public void StartJump()
    {
        EventManager.Publish("DoJump");
    }

    public void StopJump()
    {
        EventManager.Publish("LockMovement", false);
        EventManager.Publish("ResetJump");
    }
    
    public void DropController()
    {
        EventManager.Publish("LockMovement", true);
    }
}
