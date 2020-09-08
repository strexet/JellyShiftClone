using JellyShift.Player.Controller;
using JellyShift.Player.Movement;

namespace JellyShift.Player.States
{
    public class CollisionMovement : IPlayerState
    {
        private readonly IMover _collisionMover;
        private readonly IPlayerEventsNotifier _playerEventsNotifier;

        private IPlayerState _physicalMovement;

        public event PlayerStateDelegate StateChanged = delegate { };

        public CollisionMovement(CollisionMover collisionMover, IPlayerEventsNotifier playerEventsNotifier)
        {
            _collisionMover = collisionMover;
            _playerEventsNotifier = playerEventsNotifier;
        }

        public void Init(PhysicalMovement physicalMovement)
        {
            _physicalMovement = physicalMovement;
        }

        public void PrepareState()
        {
            _collisionMover.Init();
            _playerEventsNotifier.CollisionFinished += OnCollisionFinished;
        }

        public void ResetState()
        {
            _playerEventsNotifier.CollisionFinished -= OnCollisionFinished;
        }

        public void UpdateState()
        {
            _collisionMover.Move();
        }

        private void OnCollisionFinished() => StateChanged.Invoke(_physicalMovement);
    }
}