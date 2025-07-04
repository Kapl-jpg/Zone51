using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSystem : MonoBehaviour
{
    [SerializeField] private List<GeneratorData> points;
    [SerializeField] private float moveTime;
    [SerializeField] private float endMoveTime;
    
    [SerializeField] private MeshRenderer firstWirePart;
    [SerializeField] private MeshRenderer secondWirePart;
    [SerializeField] private MeshRenderer thirdWirePart;
    
    [SerializeField] [ColorUsage(false,true)] 
    private Color lightColor;
    
    private const int MAX_CHARGES = 3;
    private int _chargeLevels;

    private void Start()
    {
        var fM = firstWirePart.material;
        var sM = secondWirePart.material;
        var tM = thirdWirePart.material;
        
        firstWirePart.material = new Material(fM);
        secondWirePart.material = new Material(sM);
        thirdWirePart.material = new Material(tM);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_chargeLevels >= MAX_CHARGES) return;

        if (!other.CompareTag("Battery")) return;

        StartCoroutine(SetCharge(other.gameObject, _chargeLevels));
        _chargeLevels++;
    }

    private IEnumerator SetCharge(GameObject go, int index)
    {
        go.TryGetComponent(out Rigidbody rb);
        rb.isKinematic = true;

        go.TryGetComponent(out Collider col);
        col.isTrigger = true;

        go.tag = "Untagged";
        
        var t = 0f;
        var startPosition = go.transform.position;
        var startRotation = go.transform.rotation;
        
        while (t < 1)
        {
            t += Time.deltaTime / moveTime;
            go.transform.position = Vector3.Lerp(startPosition, points[index].startPoint.position, t);
            go.transform.rotation = Quaternion.Lerp(startRotation, points[index].startPoint.rotation, t);
            yield return null;
        }

        var f = 0f;
        while (f < 1)
        {
            f += Time.deltaTime / endMoveTime;
            go.transform.position = Vector3.Lerp(points[index].startPoint.position, points[index].endPoint.position, f);
            
            yield return null;
        }

        switch (index)
        {
            case 0:
                firstWirePart.material.SetColor("_Color", lightColor);
                break;
            case 1:
                secondWirePart.material.SetColor("_Color", lightColor);
                break;
            case 2:
                thirdWirePart.material.SetColor("_Color", lightColor);
                EventManager.Publish("EnableRotateMechanism");
                break;
        }
    }
}