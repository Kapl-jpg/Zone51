using UnityEngine;

namespace Player
{
    public class GroundChecker : Subscriber
    {
        [SerializeField] private Transform mainPlayer;
        [SerializeField] private float groundRadius;
        [SerializeField] private float groundDistance;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private bool draw;
        private bool _landing;
        
        [Request("IsGrounded")]
        private ObservableField<bool> _isGrounded = new();

        private void Update()
        {
            _isGrounded.Value = Physics.SphereCast(transform.position, groundRadius, -Vector3.up, out RaycastHit hit, groundDistance, layerMask);
            if (!_isGrounded.Value)
            {
                _landing = true;
            }
            else
            {
                if (_landing)
                {
                    EventManager.Publish("Landing");
                    EventManager.Publish("ActiveAudioJumpEnd");
                    _landing = false;
                }
            }
            mainPlayer.parent = _isGrounded.Value ? hit.transform : null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position - Vector3.up * groundDistance, groundRadius);
        }
    }
}