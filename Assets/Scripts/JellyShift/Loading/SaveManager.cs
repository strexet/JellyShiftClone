using UnityEngine;

namespace JellyShift.Loading
{
    public class SaveManager
    {
        private const string LevelIndexKey = "LevelIndex";

        public int GetCurrentLevelIndex()
        {
            var currentLevelIndex = PlayerPrefs.GetInt(LevelIndexKey, 0);
            return currentLevelIndex;
        }

        public int SaveNextLevelIndex()
        {
            var levelIndex = GetCurrentLevelIndex();

            levelIndex++;

            PlayerPrefs.SetInt(LevelIndexKey, levelIndex);
            PlayerPrefs.Save();

            return levelIndex;
        }
    }
}