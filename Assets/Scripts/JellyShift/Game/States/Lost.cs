using JellyShift.JellyInput;
using JellyShift.Loading;
using JellyShift.UI;
using UnityEngine;

namespace JellyShift.Game.States
{
    public class Lost : IGameState
    {
        private readonly LostPopup _lostPopup;
        private readonly LevelManager _levelManager;
        private readonly ITouchInput _input;
        private IGameState _menu;

        public event GameStateDelegate StateChanged = delegate { };

        public Lost(LevelManager levelManager, LostPopup lostPopup, ITouchInput input)
        {
            _levelManager = levelManager;
            _lostPopup = lostPopup;
            _input = input;
        }

        public void Init(Menu menu)
        {
            _menu = menu;
        }

        public void PrepareState()
        {
            Time.timeScale = 0;
            _input.Disable();
            _lostPopup.Show(OnTouch);
        }

        public void ResetState()
        {
            _lostPopup.Hide();
            Time.timeScale = 1;
            _levelManager.LoadLevel();
        }

        public void UpdateState() { }

        private void OnTouch() => StateChanged.Invoke(_menu);
    }
}