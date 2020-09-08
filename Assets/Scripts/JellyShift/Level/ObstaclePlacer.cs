using System.Collections.Generic;
using JellyShift.Obstacles;
using UnityEngine;

namespace JellyShift.Level
{
    public class ObstaclePlacer : MonoBehaviour
    {
        [SerializeField] private float _distanceBetweenObstacles = 2.5f;
        [SerializeField] private List<Obstacle> _obstacles;

        private void OnValidate()
        {
            InitObstacles();
            PlaceObstacles();
        }

        private void InitObstacles()
        {
            var obstacles = GetComponentsInChildren<Obstacle>();
            _obstacles = new List<Obstacle>(obstacles.Length);
            _obstacles.AddRange(obstacles);
        }

        private void PlaceObstacles()
        {
            for (var i = 0; i < _obstacles.Count; i++)
            {
                var obstacle = _obstacles[i];

                var position = new Vector3(i * _distanceBetweenObstacles, 0, 0);
                var rotation = Quaternion.identity;

                var obstacleTransform = obstacle.transform;
                obstacleTransform.localPosition = position;
                obstacleTransform.localRotation = rotation;
            }
        }
    }
}