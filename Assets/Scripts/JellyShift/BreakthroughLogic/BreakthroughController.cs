using System.Collections;
using JellyShift.Player.Controller;
using JellyShift.Progress;
using UnityEngine;

namespace JellyShift.BreakthroughLogic
{
    public class BreakthroughController : MonoBehaviour, IBreakthroughController
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private StreakManager _streakManager;
        [SerializeField] private PlayerController _playerController;

        private bool _isBreakthroughActive;

        public event BreakthroughDelegate BreakthroughStarted = delegate { };
        public event BreakthroughDelegate BreakthroughFinished = delegate { };

        private void OnEnable()
        {
            _streakManager.StreakFilled += StartBreakthrough;
        }

        private void OnDisable()
        {
            _streakManager.StreakFilled -= StartBreakthrough;
        }

        public void UpdateController()
        {
            _playerController.UpdateController();
        }

        private void StartBreakthrough()
        {
            if (_isBreakthroughActive) return;

            _mainCamera.backgroundColor = _gameSettings.BreakthroughBackgroundColor;

            _isBreakthroughActive = true;
            BreakthroughStarted.Invoke();
            StartCoroutine(BreakthroughRoutine());
        }

        private IEnumerator BreakthroughRoutine()
        {
            yield return new WaitForSeconds(_gameSettings.BreakthroughDuration);
            StopBreakthrough();
        }

        public void StopBreakthrough()
        {
            if (!_isBreakthroughActive) return;

            _mainCamera.backgroundColor = _gameSettings.NormalBackgroundColor;

            _isBreakthroughActive = false;
            BreakthroughFinished.Invoke();
        }
    }
}