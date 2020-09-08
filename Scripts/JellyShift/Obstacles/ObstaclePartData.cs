using UnityEngine;

namespace JellyShift.Obstacles
{
    [CreateAssetMenu(fileName = "ObstaclePartData", menuName = "Game/Obstacle/ObstaclePartData", order = 0)]
    public class ObstaclePartData : ScriptableObject
    {
        [Header("Physics")]
        public int TransparentLayer = 9;
        public float Mass = 1;

        [Header("Forces")]
        public float RandomForceAmplitude = 12;
        public float ExplosionForceAmplitude = 50;
        public float ExplosionRadius = 3;

        [Header("Colors")]
        public Color NormalColor1;
        public Color NormalColor2;
        public Color BreakthroughColor1;
        public Color BreakthroughColor2;
    }
}