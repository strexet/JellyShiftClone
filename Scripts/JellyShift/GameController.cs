using JellyShift.BreakthroughLogic;
using JellyShift.Game.States;
using JellyShift.JellyInput;
using JellyShift.Loading;
using JellyShift.Player.Animation;
using JellyShift.Player.Controller;
using JellyShift.Player.Manager;
using JellyShift.UI;
using UnityEngine;

namespace JellyShift
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerEventsNotifier _playerEventsNotifier;
        [SerializeField] private BreakthroughController _breakthroughController;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private TouchInput _touchInput;
        [SerializeField] private LostPopup _lostPopup;

        private IGameState _currentState;

        private void Awake()
        {
            InitGameStates();
        }

        private void FixedUpdate()
        {
            _currentState.UpdateState();
        }

        private void InitGameStates()
        {
            var menu = new Menu(_touchInput, _playerManager, _playerAnimator);
            var start = new Start(_playerAnimator, _touchInput);
            var normalProcessing = new NormalProgressing(_playerEventsNotifier, _playerController, _breakthroughController);
            var breakthrough = new Breakthrough(_breakthroughController, _playerEventsNotifier);
            var lost = new Lost(_levelManager, _lostPopup, _touchInput);
            var finish = new Finish(_levelManager, _playerAnimator, _touchInput);

            menu.Init(start);
            start.Init(normalProcessing);
            normalProcessing.Init(finish, lost, breakthrough);
            breakthrough.Init(finish, lost, normalProcessing);
            lost.Init(menu);
            finish.Init(menu);

            OnStateChanged(menu);
        }

        private void OnStateChanged(IGameState newState)
        {
            if (_currentState != null)
            {
                _currentState.StateChanged -= OnStateChanged;
                _currentState.ResetState();
            }

            Debug.Log($"GameController.OnStateChanged> GAME: {_currentState?.GetType().Name} --> {newState.GetType().Name}");

            newState.StateChanged += OnStateChanged;
            newState.PrepareState();

            _currentState = newState;
        }
    }
}