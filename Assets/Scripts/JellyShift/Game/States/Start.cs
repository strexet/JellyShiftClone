using JellyShift.JellyInput;
using JellyShift.Player.Animation;

namespace JellyShift.Game.States
{
    public class Start : IGameState
    {
        private readonly IPlayerAnimator _animator;
        private readonly ITouchInput _input;
        private IGameState _normalProcessing;

        public event GameStateDelegate StateChanged = delegate { };

        public Start(IPlayerAnimator animator, ITouchInput input)
        {
            _animator = animator;
            _input = input;
        }

        public void Init(NormalProgressing normalProcessing)
        {
            _normalProcessing = normalProcessing;
        }

        public void PrepareState()
        {
            _input.Disable();
            _animator.FinishedAnimation += OnFinishedAnimation;
        }

        public void ResetState()
        {
            _animator.FinishedAnimation -= OnFinishedAnimation;
            _input.Enable();
        }

        public void UpdateState()
        {
            _animator.PlayStartAnimation();
        }

        private void OnFinishedAnimation() => StateChanged.Invoke(_normalProcessing);
    }
}