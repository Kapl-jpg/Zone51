using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    public void OpenDoor()
    {
        EventManager.Publish("OpenMainDoor");
    }

    public void OpenExperimentDoor()
    {
        EventManager.Publish("OpenExperimentDoor");
    }
}