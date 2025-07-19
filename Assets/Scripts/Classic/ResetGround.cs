using UnityEngine;

public class ResetGround : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        EventManager.Publish("ResetPlate");
    }
}