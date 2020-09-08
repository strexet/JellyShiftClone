using UnityEngine;

namespace JellyShift.Player.Movement
{
    public class PhysicalMover : MonoBehaviour, IMover
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GameSettings _gameSettings;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void Init()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        public void Move()
        {
            var forwardVelocity = _gameSettings.PlayerPhysicalSpeed * _transform.forward;
            var verticalVelocity = _rigidbody.velocity.y * Vector3.up;

            var velocity = forwardVelocity + verticalVelocity;
            _rigidbody.velocity = velocity;
        }
    }
}