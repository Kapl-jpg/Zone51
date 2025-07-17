using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    [SerializeField] private float flickerDelay = 0.1f;

    private Light lightSource;

    private float timerFlicker;
    private float lightIntensity;

    private void Start()
    {
        lightSource = GetComponent<Light>();

        lightIntensity = lightSource.intensity;
        timerFlicker = flickerDelay;
    }

    public void FlashingLights(bool active)
    {
        if (active)
        {
            timerFlicker -= Time.deltaTime;
            if (timerFlicker <= 0)
            {
                lightSource.intensity = 0;
                timerFlicker = flickerDelay;
            }
            else
            {
                lightSource.intensity = lightIntensity;
            }
        }
        else
        {
            lightSource.intensity = lightIntensity;
        }
    }
}
