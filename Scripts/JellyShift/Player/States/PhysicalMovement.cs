using JellyShift.Player.Controller;
using JellyShift.Player.Movement;

namespace JellyShift.Player.States
{
    public class PhysicalMovement : IPlayerState
    {
        private readonly IMover _physicalMover;
        private readonly IPlayerEventsNotifier _playerEventsNotifier;

        private IPlayerState _pathMovement;
        private IPlayerState _playerCollision;

        public event PlayerStateDelegate StateChanged = delegate { };

        public PhysicalMovement(PhysicalMover physicalMover, IPlayerEventsNotifier playerEventsNotifier)
        {
            _physicalMover = physicalMover;
            _playerEventsNotifier = playerEventsNotifier;
        }

        public void Init(PathMovement pathMovement, CollisionMovement collisionMovement)
        {
            _pathMovement = pathMovement;
            _playerCollision = collisionMovement;
        }

        public void PrepareState()
        {
            _physicalMover.Init();
            _playerEventsNotifier.CollisionStarted += OnCollisionStarted;
            _playerEventsNotifier.EnteredTransitionZone += OnEnteredTransitionZone;
        }

        public void ResetState()
        {
            _playerEventsNotifier.CollisionStarted -= OnCollisionStarted;
            _playerEventsNotifier.EnteredTransitionZone -= OnEnteredTransitionZone;
        }

        public void UpdateState()
        {
            _physicalMover.Move();
        }

        private void OnCollisionStarted() => StateChanged.Invoke(_playerCollision);
        private void OnEnteredTransitionZone() => StateChanged.Invoke(_pathMovement);
    }
}