using UnityEngine.SceneManagement;

namespace JellyShift.Loading
{
    public class LevelLoader
    {
        private readonly string _levelPrefix;
        private readonly int _maxLevelCount;

        public LevelLoader(string levelPrefix, int maxLevelCount)
        {
            _maxLevelCount = maxLevelCount;
            _levelPrefix = levelPrefix;
        }

        public void LoadLevel(int levelIndex)
        {
            var realIndex = levelIndex % _maxLevelCount;
            var levelName = $"{_levelPrefix}{realIndex}";
            var loadSceneOperation = SceneManager.LoadSceneAsync(levelName);
        }
    }
}