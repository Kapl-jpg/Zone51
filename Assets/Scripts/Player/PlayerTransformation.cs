using System.Collections;
using Enums;
using UnityEngine;

public class PlayerTransformation : Subscriber
{
    [SerializeField] private float stayTime;
    [SerializeField] private GameObject alienForm;
    [SerializeField] private GameObject humanForm;
    [SerializeField] private SkinnedMeshRenderer alienSkinnedMeshRenderer;
    [SerializeField] private SkinnedMeshRenderer humanSkinnedMeshRenderer;
    [SerializeField] private AudioSource audioTransformation;

    private Material _alienMaterial;
    private Material _humanMaterial;

    private void Start()
    {
        var aMat = alienSkinnedMeshRenderer.material;
        var ahMat = humanSkinnedMeshRenderer.material;
        _alienMaterial = aMat;
        _humanMaterial = ahMat;
        alienSkinnedMeshRenderer.material = _alienMaterial;
        humanSkinnedMeshRenderer.material = _humanMaterial;
    }

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
        EventManager.Publish("CameraForHuman");
        EventManager.Publish("Transformation", true);

        audioTransformation.Play();

        _alienMaterial.SetFloat("_Reverse", 1f);
        _humanMaterial.SetFloat("_Reverse", 0f);
        
        float t = 1;
        while (t > 0)
        {
            t = Mathf.Clamp01(t - Time.deltaTime / stayTime);
            _alienMaterial.SetFloat("_DissolveValue", t);
            _humanMaterial.SetFloat("_DissolveValue", t);

            yield return null;
        }

        EventManager.Publish("Transformation", false);
        EventManager.Publish("SetForm", CharacterType.Human);
        alienForm.SetActive(false);
    }

    private IEnumerator StayAlien()
    {
        alienForm.TryGetComponent(out Collider alienCol);
        humanForm.TryGetComponent(out Collider humanCol);

        alienCol.enabled = true;
        humanCol.enabled = false;

        alienForm.SetActive(true);
        EventManager.Publish("CameraForAlien");
        EventManager.Publish("Transformation", true);

        audioTransformation.Play();

        _alienMaterial.SetFloat("_Reverse", 0f);
        _humanMaterial.SetFloat("_Reverse", 1f);

        float t = 0;
        while (t < 1)
        {
            t = Mathf.Clamp01(t + Time.deltaTime / stayTime);
            _alienMaterial.SetFloat("_DissolveValue", 1 - t);
            _humanMaterial.SetFloat("_DissolveValue", 1 - t);

            yield return null;
        }
        
        EventManager.Publish("Transformation", false);
        EventManager.Publish("SetForm", CharacterType.Alien);
        humanForm.SetActive(false);
    }
}