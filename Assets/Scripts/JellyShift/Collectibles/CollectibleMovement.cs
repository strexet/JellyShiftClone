using UnityEngine;

namespace JellyShift.Collectibles
{
    public class CollectibleMovement : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 3.5f;
        [SerializeField] private float _verticalOffsetSpeed = 0.01f;
        [SerializeField] private float _maxVerticalOffset = 0.1f;

        private Transform _transform;
        private float _currentOffset;
        private bool _isMovingUp;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (Mathf.Abs(_currentOffset) >= _maxVerticalOffset)
            {
                _isMovingUp = !_isMovingUp;
            }

            var up = _isMovingUp ? 1 : -1;
            _currentOffset += _verticalOffsetSpeed * up;
            _transform.localRotation *= Quaternion.AngleAxis(_rotationSpeed, Vector3.up);
        }
    }
}