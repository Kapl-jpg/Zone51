using System.Collections;
using Interfaces;
using UnityEngine;

namespace Generator
{
    public class RotatePuzzleButton : Subscriber, IInteractable
    {
        [SerializeField] private Transform mechanism;
        [SerializeField] private float rotateTime;
        
        [SerializeField] [ColorUsage(false, true)]
        private Color lightColor;
        private Color _defaultColor = Color.white;

        [Request("GetPower")] private ObservableField<bool> _getPower = new();
        [Header("Materials")] 
        [SerializeField] private MeshRenderer mainWire;
        [SerializeField] private MeshRenderer[] upWire;
        [SerializeField] private MeshRenderer[] leftWire;
        [SerializeField] private MeshRenderer[] rightWire;
        [SerializeField] private MeshRenderer upMech;
        [SerializeField] private MeshRenderer rightMech;
        [SerializeField] private MeshRenderer downMech;
        [SerializeField] private AudioSource audioRotateMechanisn;
        [SerializeField] private AudioSource audioOnWires;
        [SerializeField] private Collider triggerCollider;

        private Directions _currentDirection = Directions.Right;

        private bool _canUse;
        private bool _locked;

        private void Start()
        {
            var mW = mainWire.material;
            var uM =  upMech.material;
            var lM = rightMech.material;
            var dM = downMech.material;

            mainWire.material = new Material(mW);
            foreach (var mesh in upWire)
            {
                mesh.material = new Material(mesh.material);
            }

            foreach (var mesh in leftWire)
            {
                mesh.material = new Material(mesh.material);
            }

            foreach (var mesh in rightWire)
            {
                mesh.material = new Material(mesh.material);
            }
            upMech.material = new Material(uM);
            rightMech.material = new Material(lM);
            downMech.material = new Material(dM);
        }

        [Event("EnableRotateMechanism")]
        private void EnableRotate()
        {
            mainWire.material.SetColor("_Color", lightColor);
            foreach (var mesh in upWire)
            {
                mesh.material.SetColor("_Color", lightColor);
            }
            foreach (var mesh in rightWire)
            {
                mesh.material.SetColor("_Color", lightColor);
            }
            
            upMech.material.SetColor("_Color", lightColor);
            rightMech.material.SetColor("_Color", lightColor);
            downMech.material.SetColor("_Color", lightColor);
            
            triggerCollider.enabled = true;
            _canUse = true;
        }

        public void Interact()
        {
            if(!_canUse) return;
            
            if(!_locked)
                StartCoroutine(Rotate());
        }

        private IEnumerator Rotate()
        {
            _locked = true;
            var angle = mechanism.rotation;
            var targetAngle = Quaternion.Euler(0, 0, TargetAngle());

            audioRotateMechanisn.Play();
            yield return new WaitForSeconds(0.1f);
            DisableWires();
            
            var t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / rotateTime;
                mechanism.transform.rotation = Quaternion.Lerp(angle, targetAngle, t);
                yield return null;
            }

            if (_currentDirection == Directions.Up)
            {
                mechanism.transform.rotation = Quaternion.identity;
            }
            
            EnableWires();
            _currentDirection = GetNextDirection();
            
            _locked = false;
        }

        private void DisableWires()
        {
            foreach (var mesh in upWire)
            {
                mesh.material.SetColor("_Color", _defaultColor);
            }
            foreach (var mesh in leftWire)
            {
                mesh.material.SetColor("_Color", _defaultColor);
            }
            foreach (var mesh in rightWire)
            {
                mesh.material.SetColor("_Color", _defaultColor);
            }
            
            upMech.material.SetColor("_Color", _defaultColor);
            rightMech.material.SetColor("_Color", _defaultColor);
            downMech.material.SetColor("_Color", _defaultColor);
            
            EventManager.Publish("DisablePowerScreen");
            _getPower.Value = false;
        }
        
        private void EnableWires()
        {
            switch (_currentDirection)
            {
                case Directions.Up:
                    
                    foreach (var mesh in upWire)
                    {
                        audioOnWires.Play();
                        mesh.material.SetColor("_Color", lightColor);
                    }
                    
                    foreach (var mesh in rightWire)
                    {
                        audioOnWires.Play();
                        mesh.material.SetColor("_Color", lightColor);
                    }
                    
                    EnableInnerParts();
                    break;
                case Directions.Right:
                    
                    foreach (var mesh in leftWire)
                    {
                        audioOnWires.Play();
                        mesh.material.SetColor("_Color", lightColor);
                    }
                    
                    foreach (var mesh in rightWire)
                    {
                        audioOnWires.Play();
                        mesh.material.SetColor("_Color", lightColor);
                    }

                    EventManager.Publish("EnablePowerScreen");
                    EnableInnerParts();
                    break;
                case Directions.Down:
                    
                    foreach (var mesh in upWire)
                    {
                        audioOnWires.Play();
                        mesh.material.SetColor("_Color", lightColor);
                    }
                    foreach (var mesh in leftWire)
                    {
                        audioOnWires.Play();
                        mesh.material.SetColor("_Color", lightColor);
                    }

                    _getPower.Value = true;
                    EventManager.Publish("EnablePowerScreen");
                    EnableInnerParts();
                    break;
            }
        }

        private void EnableInnerParts()
        {
            upMech.material.SetColor("_Color", lightColor);
            rightMech.material.SetColor("_Color", lightColor);
            downMech.material.SetColor("_Color", lightColor);
        }

        private float TargetAngle()
        {
            return _currentDirection switch
            {
                Directions.Right => -90f,
                Directions.Down => -180f,
                Directions.Left => -270f,
                _ => -360f
            };
        }

        private Directions GetNextDirection()
        {
            return _currentDirection switch
            {
                Directions.Right => Directions.Down,
                Directions.Down => Directions.Left,
                Directions.Left => Directions.Up,
                _ => Directions.Right
            };
        }
    }
}