using JellyShift.Obstacles;
using UnityEngine;

namespace JellyShift.BreakthroughLogic
{
    public class BreakthroughObstacleBehaviour : MonoBehaviour
    {
        [SerializeField] private Obstacle _obstacle;

        public void StartBreakthrough()
        {
            _obstacle.PlayerContacted += BreakObstacle;
            _obstacle.SwapMaterials();
        }

        public void StopBreakthrough()
        {
            _obstacle.PlayerContacted -= BreakObstacle;
            _obstacle.SwapMaterials();
        }

        private void OnDisable()
        {
            _obstacle.PlayerCollided -= BreakObstacle;
        }

        private void BreakObstacle()
        {
            _obstacle.Break();
        }

        private void OnValidate()
        {
            if (_obstacle == null)
            {
                _obstacle = GetComponent<Obstacle>();
            }
        }
    }
}