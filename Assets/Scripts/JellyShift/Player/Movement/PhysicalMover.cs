using UnityEngine;

namespace JellyShift.Player.Movement
{
    public class PhysicalMover : MonoBehaviour, IMover
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GameSettings _gameSettings;

        private Transform _transform;
        private GameObject _gameObject;
        private MoveDirection _moveDirection;

        private void Awake()
        {
            _transform = transform;
            _gameObject = gameObject;
            _moveDirection = MoveDirection.X;
        }

        public void Setup(MoveDirection moveDirectionAfterTransition)
        {
            _moveDirection = moveDirectionAfterTransition;
        }

        public void Init()
        {
            _gameObject.layer = _gameSettings.NormalPlayerLayerIndex;
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;

            var constraints = RigidbodyConstraints.FreezeRotation;

            switch (_moveDirection)
            {
                case MoveDirection.X:
                    constraints |= RigidbodyConstraints.FreezePositionZ;
                    break;
                case MoveDirection.Z:
                    constraints |= RigidbodyConstraints.FreezePositionX;
                    break;
            }

            _rigidbody.constraints = constraints;
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