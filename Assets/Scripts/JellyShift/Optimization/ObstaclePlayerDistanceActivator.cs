using JellyShift.Obstacles;
using JellyShift.Player.Manager;
using UnityEngine;

namespace JellyShift.Optimization
{
    public class ObstaclePlayerDistanceActivator : MonoBehaviour
    {
        [SerializeField] private SlowUpdateTimeData _timeData;
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private Obstacle _obstacle;
        [SerializeField] private PlayerManager _playerManager;

        private Transform _transform;
        private float _nextCheckTime;
        private bool _isChecking;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _obstacle.PlayerPassed += OnPassedObstacle;
        }

        private void OnDisable()
        {
            _obstacle.PlayerPassed -= OnPassedObstacle;
        }

        private void FixedUpdate()
        {
            TryCheckDistanceToPlayer();
        }

        private void OnPassedObstacle()
        {
            _isChecking = true;
        }

        private void TryCheckDistanceToPlayer()
        {
            if (!_isChecking) return;

            var currentTime = Time.time;

            if (currentTime < _nextCheckTime) return;

            _nextCheckTime = currentTime + _timeData.PlayerDistanceCheck;
            CheckDistanceToPlayer();
        }

        private void CheckDistanceToPlayer()
        {
            var distance = Vector3.Distance(_playerManager.PlayerPosition, _transform.position);

            if (distance > _gameSettings.PlayerDistanceThreshold)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnValidate()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
        }
    }
}