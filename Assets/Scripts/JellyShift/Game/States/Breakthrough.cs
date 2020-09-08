using JellyShift.BreakthroughLogic;
using JellyShift.Player.Controller;

namespace JellyShift.Game.States
{
    public class Breakthrough : IGameState
    {
        private readonly IBreakthroughController _breakthroughController;
        private readonly IPlayerController _playerController;
        private readonly IPlayerEventsNotifier _playerEventsNotifier;

        private IGameState _normalProgressing;
        private IGameState _finish;
        private IGameState _lost;

        public event GameStateDelegate StateChanged = delegate { };

        public Breakthrough(IBreakthroughController breakthroughController, IPlayerEventsNotifier playerEventsNotifier)
        {
            _breakthroughController = breakthroughController;
            _playerEventsNotifier = playerEventsNotifier;
        }

        public void Init(Finish finish, Lost lost, NormalProgressing normalProgressing)
        {
            _normalProgressing = normalProgressing;
            _finish = finish;
            _lost = lost;
        }

        public void PrepareState()
        {
            _breakthroughController.BreakthroughFinished += OnBreakthroughFinished;
            _playerEventsNotifier.EnteredFinishZone += OnEnteredFinishZone;
            _playerEventsNotifier.EnteredLoseZone += OnEnteredLoseZone;
        }

        public void ResetState()
        {
            _breakthroughController.BreakthroughFinished -= OnBreakthroughFinished;
            _playerEventsNotifier.EnteredFinishZone -= OnEnteredFinishZone;
            _playerEventsNotifier.EnteredLoseZone -= OnEnteredLoseZone;

            _breakthroughController.StopBreakthrough();
        }

        public void UpdateState()
        {
            _breakthroughController.UpdateController();
        }

        private void OnBreakthroughFinished() => StateChanged.Invoke(_normalProgressing);
        private void OnEnteredFinishZone() => StateChanged.Invoke(_finish);
        private void OnEnteredLoseZone() => StateChanged.Invoke(_lost);
    }
}