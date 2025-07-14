using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioForPlayer : MonoBehaviour
{
    public void AudioWalkPlayer()
    {
        EventManager.Publish("PlayWalkingOrRunningSound");
    }
}
