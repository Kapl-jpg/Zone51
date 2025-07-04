using System.Collections;
using UnityEngine;

namespace Classic
{
    public class PlateAnimation : Subscriber
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private float transitionDuration;
        
        private Vector3 _startPoint;
        private Vector3 _endPoint;
        
        private float _t;
        private void Start()
        {
            _startPoint = transform.position;
            _endPoint = transform.position + offset;
        }

        public void Enable()
        {
            StopCoroutine(DisablePlate());
            StartCoroutine(EnablePlate());
        }

        public void Disable()
        {
            StartCoroutine(EnablePlate());
            StartCoroutine(DisablePlate());
        }

        private IEnumerator EnablePlate()
        {
            while (_t < 1)
            {
                _t += Time.deltaTime / transitionDuration;
                transform.position = Vector3.Lerp(_startPoint, _endPoint, _t);
                yield return null;
            }
        }

        private IEnumerator DisablePlate()
        {
            while (_t > 0)
            {
                _t -= Time.deltaTime / transitionDuration;
                transform.position = Vector3.Lerp(_startPoint, _endPoint, _t);
                yield return null;
            }
        }
    }
}