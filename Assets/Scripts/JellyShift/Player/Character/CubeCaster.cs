using JellyShift.Optimization;
using UnityEngine;

namespace JellyShift.Player.Character
{
    public class CubeCaster : MonoBehaviour
    {
        [SerializeField] private LayerMask _castTargetLayers;
        [SerializeField] private GameSettings _gameSettings;

        [SerializeField] private SlowUpdateTimeData _timeData;

        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _jellyCubeTransform;
        [SerializeField] private GameObject _castedCube;
        [SerializeField] private Transform _castedViewTransform;

        private Transform _castedCubeTransform;
        private float _nextCastTime;
        private bool _isCasting;

        private void OnEnable()
        {
            _castedCubeTransform = _castedCube.transform;
            _castedCube.SetActive(false);
        }

        private void Update()
        {
            TryCastRay();
            CopyCubeForm();
        }

        private void CopyCubeForm()
        {
            if (!_isCasting) return;

            _castedViewTransform.localPosition = _jellyCubeTransform.localPosition;
            _castedViewTransform.localRotation = _jellyCubeTransform.localRotation;
            _castedViewTransform.localScale = _jellyCubeTransform.localScale;
        }

        private void TryCastRay()
        {
            var currentTime = Time.time;

            if (currentTime < _nextCastTime) return;

            _nextCastTime = currentTime + _timeData.CubeCast;
            CastRay();
        }

        private void CastRay()
        {
            var ray = new Ray(_playerTransform.position, _playerTransform.forward);

            if (Physics.Raycast(ray, out var hit, _gameSettings.CubeCastDistance, _castTargetLayers))
            {
                _castedCubeTransform.SetPositionAndRotation(hit.point, _playerTransform.rotation);
                SetCasting(true);
            }
            else
            {
                SetCasting(false);
            }
        }

        private void SetCasting(bool isActive)
        {
            if (_isCasting == isActive) return;

            _isCasting = isActive;
            _castedCube.SetActive(isActive);
        }
    }
}