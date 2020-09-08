using JellyShift.JellyInput;
using JellyShift.Player.Animation;
using JellyShift.Player.Manager;

namespace JellyShift.Game.States
{
    public class Menu : IGameState
    {
        private readonly ITouchInput _touchInput;
        private readonly PlayerManager _playerManager;
        private readonly PlayerAnimator _playerAnimator;
        private IGameState _start;

        public event GameStateDelegate StateChanged = delegate { };

        public Menu(TouchInput touchInput, PlayerManager playerManager, PlayerAnimator playerAnimator)
        {
            _touchInput = touchInput;
            _playerManager = playerManager;
            _playerAnimator = playerAnimator;
        }

        public void Init(Start start)
        {
            _start = start;
        }

        public void PrepareState()
        {
            _touchInput.Enable();
            _touchInput.InputStarted += OnInputStarted;
            _playerManager.SetupPlayer();
            _playerAnimator.Init();
        }

        public void ResetState()
        {
            _touchInput.InputStarted -= OnInputStarted;
        }

        public void UpdateState() { }

        private void OnInputStarted() => StateChanged.Invoke(_start);
    }
}