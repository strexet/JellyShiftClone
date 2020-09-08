using JellyShift.Optimization;
using UnityEngine;
using UsefulTools.Performance;

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

        private SlowUpdatedTask _task;

        private Transform _castedCubeTransform;
        private bool _isCasting;

        private void OnEnable()
        {
            _castedCubeTransform = _castedCube.transform;
            _castedCube.SetActive(false);

            _task = SlowUpdateManager.CreateTask(_timeData.CubeCast, CastRay);
        }

        private void OnDisable()
        {
            _task?.Destroy();
        }

        private void Update()
        {
            if (!_isCasting) return;

            _castedViewTransform.localPosition = _jellyCubeTransform.localPosition;
            _castedViewTransform.localRotation = _jellyCubeTransform.localRotation;
            _castedViewTransform.localScale = _jellyCubeTransform.localScale;
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