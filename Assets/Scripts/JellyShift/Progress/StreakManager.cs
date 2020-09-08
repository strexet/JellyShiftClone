using JellyShift.BreakthroughLogic;
using JellyShift.Obstacles;
using UnityEngine;

namespace JellyShift.Progress
{
    public class StreakManager : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private ObstacleManager _obstacleManager;
        [SerializeField] private BreakthroughController _breakthroughController;

        private int _currentStreak;

        public event StreakDelegate StreakFilled = delegate { };

        private void OnEnable()
        {
            _obstacleManager.PlayerPassedAnyObstacle += AddStreak;
            _obstacleManager.PlayerCollidedWithObstacle += BreakStreak;
            _breakthroughController.BreakthroughFinished += BreakStreak;
        }

        private void OnDisable()
        {
            _obstacleManager.PlayerPassedAnyObstacle -= AddStreak;
            _obstacleManager.PlayerCollidedWithObstacle -= BreakStreak;
            _breakthroughController.BreakthroughFinished -= BreakStreak;
        }

        private void AddStreak()
        {
            _currentStreak++;

            if (_currentStreak >= _gameSettings.MaxStreakCount)
            {
                StreakFilled.Invoke();
            }
        }

        private void BreakStreak()
        {
            _currentStreak = 0;
        }
    }
}