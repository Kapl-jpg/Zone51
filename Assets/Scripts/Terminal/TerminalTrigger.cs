using Enums;
using UnityEngine;

public class TerminalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        EventManager.Publish("ForcedTransformation", CharacterType.Human);
        gameObject.SetActive(false);
    }
    
}
