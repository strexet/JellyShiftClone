using JellyShift.Player.Controller;
using JellyShift.Player.Movement;

namespace JellyShift.Player.States
{
    public class PathMovement : IPlayerState
    {
        private readonly IMover _pathMover;
        private readonly IPlayerEventsNotifier _playerEventsNotifier;

        private IPlayerState _physicalMovement;

        public event PlayerStateDelegate StateChanged = delegate { };

        public PathMovement(PathMover pathMover, IPlayerEventsNotifier playerEventsNotifier)
        {
            _pathMover = pathMover;
            _playerEventsNotifier = playerEventsNotifier;
        }

        public void Init(PhysicalMovement physicalMovement)
        {
            _physicalMovement = physicalMovement;
        }

        public void PrepareState()
        {
            _pathMover.Init();
            _playerEventsNotifier.EscapedTransitionZone += OnEscapedTransitionZone;
        }

        public void ResetState()
        {
            _playerEventsNotifier.EscapedTransitionZone -= OnEscapedTransitionZone;
        }

        public void UpdateState()
        {
            _pathMover.Move();
        }

        private void OnEscapedTransitionZone() => StateChanged.Invoke(_physicalMovement);
    }
}