using PathCreation;
using UnityEngine;

namespace JellyShift.Level.Zones
{
    public class StartTransitionZone : TriggerZone
    {
        [SerializeField] private PathCreator _transitionPath;
        [SerializeField] private float _transitionSpeed;

        public PathCreator Path => _transitionPath;
        public float Speed => _transitionSpeed;
    }
}