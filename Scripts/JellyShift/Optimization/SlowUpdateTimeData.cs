using UnityEngine;

namespace JellyShift.Optimization
{
    [CreateAssetMenu(fileName = "SlowUpdateTimeData", menuName = "Game/Optimization/SlowUpdateTimeData", order = 0)]
    public class SlowUpdateTimeData : ScriptableObject
    {
        public float PlayerDistanceCheck = 1f;
        public float CubeCast = 0.2f;
    }
}