using System;
using System.Collections;
using UnityEngine;

public class GeneratorMiniBehaviour : MonoBehaviour
{
    [SerializeField] private float firstBatteryStep;
    [SerializeField] private float secondBatteryStep;
    [SerializeField] private float brakeBarrierTime;

    [SerializeField] private MeshRenderer generatorBarrierMesh;
    [SerializeField] private MeshRenderer generatorWireMesh;
    [SerializeField] private Collider generatorBarrierCollider;
    
    [SerializeField] [ColorUsage(false,true)] Color wireEnableColor;
    [SerializeField] Color wireDisableColor;
    private Material _generatorBarrierMaterial;
    private Material _generatorWireMaterial;
    private float _currentBatteryDissolveValue;

    private bool _disabled;
    
    private void Start()
    {
        _generatorBarrierMaterial = new Material(generatorBarrierMesh.material);
        generatorBarrierMesh.material = _generatorBarrierMaterial;
        
        _generatorWireMaterial = new Material(generatorWireMesh.material);
        generatorWireMesh.material = _generatorWireMaterial;
        
        _generatorWireMaterial.SetColor("_Color", wireEnableColor);
    }

    public void RemoveBattery()
    {
        if(!_disabled)
            StartCoroutine(Dissolve(0, secondBatteryStep));
        
    }

    private IEnumerator Dissolve(float start, float step)
    {
        _generatorWireMaterial.SetColor("_Color", wireDisableColor);
        float t = start;
        while (t < step)
        {
            t = Mathf.Clamp(t + Time.deltaTime, start, step);
            _generatorBarrierMaterial.SetFloat("_DissolveValue", t);
            yield return null;
        }
        
        _disabled = true;
        generatorBarrierCollider.enabled = false;
    }
}