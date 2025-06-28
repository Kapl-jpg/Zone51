using UnityEngine;
using UnityEngine.VFX;

public class CameraEffect : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private VisualEffect visualEffect;

    private void Update()
    {
        visualEffect.SetVector3("CamPos", target.position);
    }
}
