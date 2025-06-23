using System.Collections;
using Enums;
using UnityEngine;

public class PlayerTransformation : Subscriber
{
    [SerializeField] private float stayTime;
    [SerializeField] private GameObject alienForm;
    [SerializeField] private GameObject humanForm;
    [SerializeField] private Material alienMaterial;
    [SerializeField] private Material humanMaterial;

    [Event("ChangeForm")]
    private void ChangeForm()
    {
        var type = RequestManager.GetValue<CharacterType>("CharacterType");
        switch (type)
        {
            case CharacterType.Alien:
                StartCoroutine(StayAlien());
                break;
            case CharacterType.Human:
                StartCoroutine(StayHuman());
                break;
        }
    }

    private IEnumerator StayHuman()
    {
        alienForm.TryGetComponent(out Collider alienCol);
        humanForm.TryGetComponent(out Collider humanCol);
        
        alienCol.enabled = false;
        humanCol.enabled = true;
        
        humanForm.SetActive(true);
        float t = 0;
        while (t < 1)
        {
            t = Mathf.Clamp01(t + Time.deltaTime / stayTime);
            //alienMaterial.SetFloat("_DissolveValue", 1-t);
            humanMaterial.SetFloat("_DissolveValue", t);
            yield return null;
        }

        alienForm.SetActive(false);
    }

    private IEnumerator StayAlien()
    {
        alienForm.TryGetComponent(out Collider alienCol);
        humanForm.TryGetComponent(out Collider humanCol);
        
        alienCol.enabled = true;
        humanCol.enabled = false;
        
        alienForm.SetActive(true);
        float t = 1;
        while (t > 0)
        {
            t = Mathf.Clamp01(t - Time.deltaTime / stayTime);
            //alienMaterial.SetFloat("_DissolveValue", 1-t);
            humanMaterial.SetFloat("_DissolveValue", t);
            yield return null;
        }

        humanForm.SetActive(false);
    }
}
