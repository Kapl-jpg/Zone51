using UnityEngine;

namespace Classic
{
    public class ExperimentRoomTrigger : MonoBehaviour
    {
        [SerializeField] private Collider doorCollider;
        [SerializeField] private GameObject[] lights;
        [SerializeField] private Material tvMaterial;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            foreach (var light in lights)
            {
                light.SetActive(true);
            }
            doorCollider.enabled = true;
            EventManager.Publish("CloseExperimentDoor");
            tvMaterial.SetFloat("_Enable", 1);

            gameObject.SetActive(false);
        }

        private void OnApplicationQuit()
        {
            tvMaterial.SetFloat("_Enable", 0);
        }
    }
}