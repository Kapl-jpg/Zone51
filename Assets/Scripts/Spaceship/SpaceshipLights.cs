using System;
using System.Collections;
using UnityEngine;

public class SpaceshipLights : MonoBehaviour
{
    [SerializeField] private GameObject[] firstLights;
    [SerializeField] private GameObject[] secondLights;
    [SerializeField] private GameObject[] thirdLights;
    [SerializeField] private float flickDelay;
    [SerializeField] private float disableLightDuration;
    [SerializeField] private float disableNextLightDelay;

    private IEnumerator Start()
    {
        while (true)
        {
            foreach (var firstLight in firstLights)
            {
                firstLight.SetActive(false);
            }
            
            yield return new WaitForSeconds(disableLightDuration);
    
            foreach (var firstLight in firstLights)
            {
                firstLight.SetActive(true);
            }
    
            yield return new WaitForSeconds(disableNextLightDelay);
    
            foreach (var secondLight in secondLights)
            {
                secondLight.SetActive(false);
            }
            
            yield return new WaitForSeconds(disableLightDuration);
    
            foreach (var secondLight in secondLights)
            {
                secondLight.SetActive(true);
            }
    
            yield return new WaitForSeconds(disableNextLightDelay);
    
            foreach (var thirdLight in thirdLights)
            {
                thirdLight.SetActive(false);
            }
            
            yield return new WaitForSeconds(disableLightDuration);
    
            foreach (var thirdLight in thirdLights)
            {
                thirdLight.SetActive(true);
            }
    
            yield return new WaitForSeconds(flickDelay);
        }
    }
}
