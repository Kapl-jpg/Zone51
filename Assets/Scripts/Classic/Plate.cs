using Classic;
using Enums;
using UnityEngine;

public class Plate : Subscriber
{
    [SerializeField] private Plate nextPlate;
    [SerializeField] private PlateAnimation plateAnimation;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private CharacterType plateType;
    [SerializeField] private bool active;
    [SerializeField] private bool final;

    private AudioSource _audioClassic;
    private Material _plateMaterial;
    private bool _active;
    private bool _canUse;
    private bool _solved;
    
    private void Start()
    {
        _audioClassic = GetComponent<AudioSource>();
        _canUse = active;
        _plateMaterial = new Material(meshRenderer.material);
        meshRenderer.material = _plateMaterial;
        _plateMaterial.SetFloat("_Enable", active ? 1f : 0f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_canUse)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            var characterType = RequestManager.GetValue<CharacterType>("CharacterType");

            if (!final)
            {
                if (plateType == CharacterType.Neutral || characterType == plateType)
                {
                    if (nextPlate)
                    {
                        _audioClassic.Play();
                        plateAnimation.Enable();
                        nextPlate._canUse = true;
                        EnablePlate();
                    }

                    _active = true;
                    _canUse = false;
                }
                else
                {
                    EventManager.Publish("ResetPlate");
                }
            }
            else
            {
                FinalEvent();
            }
        }
        else
        {
            EventManager.Publish("ResetPlate");
        }
    }

    [Event("ResetPlate")]
    private void OnResetPlate()
    {
        if (_solved) return;
        
        if (_active)
        {
            _audioClassic.Play();
            plateAnimation.Disable();
            _active = false;
        }

        _plateMaterial.SetFloat("_Enable", active? 1f : 0f);
        _canUse = active;
    }

    [Event("SolvePlates")]
    private void SolvePlates()
    {
        _solved = true;
    }
    
    private void EnablePlate()
    {
        _plateMaterial.SetFloat("_Enable", 1f);
    }

    private void FinalEvent()
    {
        plateAnimation.Enable();
        EnablePlate();
        _canUse = false;
        EventManager.Publish("OpenTopDoor");
        EventManager.Publish("SolvePlates");
    }
}
