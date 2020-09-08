using JellyShift.JellyInput;
using JellyShift.Loading;
using JellyShift.Player.Animation;

namespace JellyShift.Game.States
{
    public class Finish : IGameState
    {
        private readonly LevelManager _levelManager;
        private readonly IPlayerAnimator _animator;
        private readonly ITouchInput _input;
        private IGameState _menu;

        public event GameStateDelegate StateChanged = delegate { };

        public Finish(LevelManager levelManager, IPlayerAnimator animator, ITouchInput input)
        {
            _levelManager = levelManager;
            _animator = animator;
            _input = input;
        }

        public void Init(Menu menu)
        {
            _menu = menu;
        }

        public void PrepareState()
        {
            _input.Disable();
            _animator.FinishedAnimation += OnFinishedAnimation;
            _levelManager.SetLevelToNext();
        }

        public void ResetState()
        {
            _animator.FinishedAnimation -= OnFinishedAnimation;
            _levelManager.LoadLevel();
        }

        public void UpdateState()
        {
            _animator.PlayFinishAnimation();
        }

        private void OnFinishedAnimation() => StateChanged.Invoke(_menu);
    }
}