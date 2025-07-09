using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAudioWalkingOrRunning : MonoBehaviour
{

    public void ActiveAudio()
    {
        EventManager.Publish("PlayWalkingOrRunningSound");
    }
}
