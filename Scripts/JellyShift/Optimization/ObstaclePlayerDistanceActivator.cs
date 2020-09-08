using JellyShift.Obstacles;
using JellyShift.Player.Manager;
using UnityEngine;
using UsefulTools.Performance;

namespace JellyShift.Optimization
{
    public class ObstaclePlayerDistanceActivator : MonoBehaviour
    {
        [SerializeField] private SlowUpdateTimeData _timeData;
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private Obstacle _obstacle;
        [SerializeField] private PlayerManager _playerManager;

        private Transform _transform;
        private SlowUpdatedTask _task;

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
            _task?.Destroy();
        }

        private void OnPassedObstacle()
        {
            _task = SlowUpdateManager.CreateTask(_timeData.PlayerDistanceCheck, CheckDistanceToPlayer);
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