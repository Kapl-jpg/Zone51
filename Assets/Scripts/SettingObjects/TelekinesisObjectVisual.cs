using Interfaces;
using UnityEngine;

public class TelekinesisObjectVisual : MonoBehaviour, IITelekinesisVisible
{
    private MeshRenderer _currentRenderer;

    private void Start()
    {
        TryGetComponent(out MeshRenderer renderer);
        _currentRenderer = renderer;
        _currentRenderer.materials[^1] = new Material(_currentRenderer.materials[^1]);
    }

    public void Show()
    {
        _currentRenderer.materials[^1].SetFloat("_Telekinesis", 1);
    }

    public void Hide()
    {
        _currentRenderer.materials[^1].SetFloat("_Telekinesis", 0);
    }
}