using UnityEngine;

namespace Player
{
    public class GroundChecker : Subscriber
    {
        [SerializeField] private float groundRadius;
        [SerializeField] private float groundDistance;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private bool draw;
        
        [Request("IsGrounded")]
        private ObservableField<bool> _isGrounded = new();

        private void Update()
        {
            _isGrounded.Value = Physics.SphereCast(transform.position, groundRadius, -Vector3.up, out _, groundDistance, layerMask);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position - Vector3.up * groundDistance, groundRadius);
        }
    }
}