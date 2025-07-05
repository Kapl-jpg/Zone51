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
    [SerializeField] private AudioSource audioTransformation;

    [Event("SwitchForm")]
    private void SwitchForm(CharacterType characterType)
    {
        if (characterType == CharacterType.Alien)
            StartCoroutine(StayAlien());
        
        if (characterType == CharacterType.Human)
            StartCoroutine(StayHuman());
    }

    private IEnumerator StayHuman()
    {
        alienForm.TryGetComponent(out Collider alienCol);
        humanForm.TryGetComponent(out Collider humanCol);
        
        alienCol.enabled = false;
        humanCol.enabled = true;
        
        humanForm.SetActive(true);
        EventManager.Publish("Transformation", true);

        audioTransformation.Play();


        float t = 0;
        while (t < 1)
        {
            t = Mathf.Clamp01(t + Time.deltaTime / stayTime);
            //alienMaterial.SetFloat("_DissolveValue", 1-t);
            humanMaterial.SetFloat("_DissolveValue", t);

            yield return null;
        }

        EventManager.Publish("Transformation", false);
        EventManager.Publish("SetForm", CharacterType.Human);
        EventManager.Publish("CameraForHuman");
        alienForm.SetActive(false);
    }

    private IEnumerator StayAlien()
    {
        alienForm.TryGetComponent(out Collider alienCol);
        humanForm.TryGetComponent(out Collider humanCol);
        
        alienCol.enabled = true;
        humanCol.enabled = false;
        
        alienForm.SetActive(true);
        EventManager.Publish("Transformation", true);

        audioTransformation.Play();

        float t = 1;
        while (t > 0)
        {
            t = Mathf.Clamp01(t - Time.deltaTime / stayTime);
            //alienMaterial.SetFloat("_DissolveValue", 1-t);
            humanMaterial.SetFloat("_DissolveValue", t);
            
            yield return null;
        }

        EventManager.Publish("Transformation", false);
        EventManager.Publish("SetForm", CharacterType.Alien);
        EventManager.Publish("CameraForAlien");
        humanForm.SetActive(false);
    }
}
