using JellyShift.BreakthroughLogic;
using JellyShift.Player.Controller;

namespace JellyShift.Game.States
{
    public class NormalProgressing : IGameState
    {
        private readonly IPlayerEventsNotifier _playerEventsNotifier;
        private readonly IPlayerController _playerController;
        private readonly IBreakthroughController _breakthroughController;

        private IGameState _finish;
        private IGameState _lost;
        private IGameState _breakthrough;

        public event GameStateDelegate StateChanged = delegate { };

        public NormalProgressing(IPlayerEventsNotifier playerEventsNotifier, IPlayerController playerController,
            IBreakthroughController breakthroughController)
        {
            _playerEventsNotifier = playerEventsNotifier;
            _playerController = playerController;
            _breakthroughController = breakthroughController;
        }

        public void Init(Finish finish, Lost lost, Breakthrough breakthrough)
        {
            _finish = finish;
            _lost = lost;
            _breakthrough = breakthrough;
        }

        public void PrepareState()
        {
            _playerEventsNotifier.EnteredFinishZone += OnEnteredFinishZone;
            _playerEventsNotifier.EnteredLoseZone += OnEnteredLoseZone;
            _breakthroughController.BreakthroughStarted += OnChargedBreakthrough;
        }

        public void ResetState()
        {
            _playerEventsNotifier.EnteredFinishZone -= OnEnteredFinishZone;
            _playerEventsNotifier.EnteredLoseZone -= OnEnteredLoseZone;
            _breakthroughController.BreakthroughStarted -= OnChargedBreakthrough;
        }

        public void UpdateState()
        {
            _playerController.UpdateController();
        }

        private void OnEnteredFinishZone() => StateChanged.Invoke(_finish);
        private void OnEnteredLoseZone() => StateChanged.Invoke(_lost);
        private void OnChargedBreakthrough() => StateChanged.Invoke(_breakthrough);
    }
}