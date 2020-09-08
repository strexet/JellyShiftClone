using UnityEngine;

namespace JellyShift.Loading
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;

        private LevelLoader _levelLoader;
        private SaveManager _saveManager;

        private int _currentLevel;

        private void Awake()
        {
            _levelLoader = new LevelLoader(_gameSettings.LevelPrefix, _gameSettings.LevelsCount);
            _saveManager = new SaveManager();

            _currentLevel = _saveManager.GetCurrentLevelIndex();
        }

        public void LoadLevel()
        {
            _levelLoader.LoadLevel(_currentLevel);
        }

        public void SetLevelToNext()
        {
            _currentLevel = _saveManager.SaveNextLevelIndex();
        }
    }
}