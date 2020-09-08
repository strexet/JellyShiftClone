using UnityEngine;

namespace JellyShift
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game/Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [Header("Player")]
        public float PlayerPhysicalSpeed = 5f;
        public float CubeCastDistance = 30;
        public int NormalPlayerLayerIndex = 5;
        public int CollidedPlayerLayerIndex = 8;

        [Header("Input")]
        public float MaxInputOffset = 300;

        [Header("Cube")]
        public float CubeDepth = 0.5f;
        public float MinCubeDimension = 0.35f;
        public float MaxCubeDimension = 2.15f;

        [Header("Collision")]
        public float CollisionForceBack = 250f;
        public float CollisionForceUp = 100f;
        public float CollisionStopDuration = 0.75f;
        public int ObstacleLayerIndex = 10;

        [Header("Optimization")]
        public float PlayerDistanceThreshold = 10;

        [Header("Streak and Breakthrough")]
        public int MaxStreakCount = 5;
        public float BreakthroughDuration = 10f;

        [Header("Levels")]
        public string LevelPrefix = "level_";
        public int LevelsCount = 5;

        [Header("Colors")]
        public Color NormalBackgroundColor;
        public Color BreakthroughBackgroundColor;
    }
}