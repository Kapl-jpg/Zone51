using System.Collections;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private float delay;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        EventManager.Publish("StartGame");
    }
}
