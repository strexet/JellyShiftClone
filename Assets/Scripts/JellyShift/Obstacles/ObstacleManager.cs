using System.Collections.Generic;
using UnityEngine;

namespace JellyShift.Obstacles
{
    public class ObstacleManager : MonoBehaviour
    {
        [SerializeField] private List<Obstacle> _obstacles;

        public event ObstacleEventDelegate PlayerPassedAnyObstacle = delegate { };
        public event ObstacleEventDelegate PlayerCollidedWithObstacle = delegate { };

        private void OnEnable()
        {
            foreach (var obstacle in _obstacles)
            {
                obstacle.PlayerPassed += OnPlayerPassedObstacle;
                obstacle.PlayerCollided += OnPlayerCollidedWithObstacle;
            }
        }

        private void OnPlayerPassedObstacle()
        {
            PlayerPassedAnyObstacle.Invoke();
        }

        private void OnPlayerCollidedWithObstacle()
        {
            PlayerCollidedWithObstacle.Invoke();
        }

        private void OnValidate()
        {
            var foundObstacles = FindObjectsOfType<Obstacle>();
            _obstacles = new List<Obstacle>();
            _obstacles.AddRange(foundObstacles);
        }
    }
}