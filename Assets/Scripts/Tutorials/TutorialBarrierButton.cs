using UnityEngine;

public class TutorialBarrierButton : MonoBehaviour
{
    public void DisableBarrier()
    {
        EventManager.Publish("TutorDisableBarrier");
    }
}