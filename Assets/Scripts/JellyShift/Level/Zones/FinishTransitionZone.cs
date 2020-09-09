using JellyShift.Player.Movement;
using UnityEngine;

namespace JellyShift.Level.Zones
{
    public class FinishTransitionZone : TriggerZone
    {
        [SerializeField] private MoveDirection _moveDirectionAfterTransition;

        public MoveDirection MoveDirectionAfterTransition => _moveDirectionAfterTransition;
    }
}