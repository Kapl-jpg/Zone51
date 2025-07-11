using Enums;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private TutorialType type;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        
        EventManager.Publish("ShowTutorial", type);
        gameObject.SetActive(false);
    }
}