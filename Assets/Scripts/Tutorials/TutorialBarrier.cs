using System;
using System.Collections;
using UnityEngine;

public class TutorialBarrier : Subscriber
{
    [SerializeField] private MeshRenderer barrierMesh;
    [SerializeField] private Collider barrierCollider;
    [SerializeField] private float dissolveTime;
    private Material _barrierMaterial;

    private void Start()
    {
        _barrierMaterial = new Material(barrierMesh.material);
        barrierMesh.material = _barrierMaterial;
    }

    [Event("TutorDisableBarrier")]
    private void TutorDisableBarrier()
    {
        StartCoroutine(DisableBarrier());
    }

    private IEnumerator DisableBarrier()
    {
        barrierCollider.enabled = false;
        var t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / dissolveTime;
            _barrierMaterial.SetFloat("_DissolveValue", t);
            yield return null;
        }
    }
}