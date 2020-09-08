using UnityEngine;

namespace JellyShift.Player.Movement
{
    public class CollisionMover : MonoBehaviour, IMover
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GameSettings _gameSettings;

        private bool _isStarted;

        public void Init()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _isStarted = false;
        }

        public void Move()
        {
            if (_isStarted) return;

            _isStarted = true;

            var forceBack = -1 * _gameSettings.CollisionForceBack * transform.forward;
            var forceUp = _gameSettings.CollisionForceUp * Vector3.up;
            var force = forceBack + forceUp;

            _rigidbody.AddForce(force, ForceMode.Acceleration);
        }
    }
}