using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    public void StartJump()
    {
        EventManager.Publish("DoJump");
        EventManager.Publish("ActiveAudioJumpStart");
    }

    public void StopJump()
    {
        EventManager.Publish("ResetJump");
        EventManager.Publish("ActiveAudioJumpEnd");
    }
}
