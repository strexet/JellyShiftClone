using UnityEngine;

namespace JellyShift.Obstacles
{
    public class ObstaclePart : MonoBehaviour
    {
        [SerializeField] private ObstaclePartData _data;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Obstacle _parentObstacle;
        [SerializeField] private Renderer _renderer;

        private bool _alreadyDynamic;
        private bool _isSwappedMaterials;

        public void SwapMaterials()
        {
            var materials = _renderer.materials;

            if (materials.Length == 2)
            {
                if (_isSwappedMaterials)
                {
                    materials[0] = _data.MaterialNormal1;
                    materials[1] = _data.MaterialNormal2;
                }
                else
                {
                    materials[0] = _data.MaterialBreakthrough1;
                    materials[1] = _data.MaterialBreakthrough2;
                }

                _renderer.materials = materials;
                _isSwappedMaterials = !_isSwappedMaterials;
            }
        }

        public void AddForceFrom(Vector3 origin)
        {
            ReplaceOnDynamic();
            AddRandomForceAndTorque();

            _rigidbody.AddExplosionForce(_data.ExplosionForceAmplitude, origin, _data.ExplosionRadius);
        }

        private void OnCollisionEnter()
        {
            if (_alreadyDynamic) return;

            ReplaceOnDynamic();
            AddRandomForceAndTorque();

            _parentObstacle.OnChildCollision();
        }

        private void ReplaceOnDynamic()
        {
            if (!_alreadyDynamic)
            {
                _alreadyDynamic = true;
                _rigidbody.isKinematic = false;
                _rigidbody.useGravity = true;
            }

            gameObject.layer = _data.TransparentLayer;
        }

        private void AddRandomForceAndTorque()
        {
            var amp = _data.RandomForceAmplitude;

            var force = new Vector3(Random.Range(-amp, amp), Random.Range(-amp, amp), Random.Range(-amp, amp));
            var torque = new Vector3(Random.Range(-amp, amp), Random.Range(-amp, amp), Random.Range(-amp, amp));

            _rigidbody.AddForce(force);
            _rigidbody.AddTorque(torque);
        }

        private void OnValidate()
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }

            _rigidbody.mass = _data.Mass;
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;

            if (_parentObstacle == null)
            {
                _parentObstacle = GetComponentInParent<Obstacle>();
            }
        }
    }
}