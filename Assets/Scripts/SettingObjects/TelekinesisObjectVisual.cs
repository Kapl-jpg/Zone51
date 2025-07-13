using Interfaces;
using UnityEngine;

public class TelekinesisObjectVisual : MonoBehaviour, IITelekinesisVisible
{
    [SerializeField] private MeshRenderer currentRenderer;

    private void Start()
    {
        currentRenderer.materials[^1] = new Material(currentRenderer.materials[^1]);
    }

    public void Show()
    {
        currentRenderer.materials[^1].SetFloat("_Telekinesis", 1);
    }

    public void Hide()
    {
        currentRenderer.materials[^1].SetFloat("_Telekinesis", 0);
    }
}