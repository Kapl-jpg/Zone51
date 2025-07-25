using System.Collections;
using UnityEngine;

public class Goose: MonoBehaviour
{
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject goose;
    [SerializeField] private new Collider collider;
    [SerializeField] private float showTime;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gooseClip;
    
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
        audioSource.PlayOneShot(audioSource.clip);
        collider.enabled = false;
        goose.SetActive(false);
        achievementPanel.SetActive(true);
        yield return new WaitForSeconds(showTime);
        achievementPanel.SetActive(false);
    }
}