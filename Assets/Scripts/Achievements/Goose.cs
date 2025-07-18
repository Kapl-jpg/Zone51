using System.Collections;
using UnityEngine;

public class Goose: MonoBehaviour
{
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject goose;
    [SerializeField] private new Collider collider;
    [SerializeField] private float showTime;
    [SerializeField] private MeshRenderer meshRenderer;
    
    private Material _gooseMaterial;
    private bool _show;

    private void Start()
    {
        _gooseMaterial = new Material(meshRenderer.material);
        meshRenderer.material = _gooseMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Dissolve());
        }
    }

    private IEnumerator Dissolve()
    {
        collider.enabled = false;
        goose.SetActive(false);
        achievementPanel.SetActive(true);
        yield return new WaitForSeconds(showTime);
        achievementPanel.SetActive(false);
    }
}