using UnityEngine;

public class CallJump : MonoBehaviour
{
    public void Jump()
    {
        EventManager.Publish("DoJump");
    }
}
