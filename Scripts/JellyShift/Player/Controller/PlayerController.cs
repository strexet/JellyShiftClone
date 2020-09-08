using JellyShift.Player.Movement;
using JellyShift.Player.States;
using UnityEngine;

namespace JellyShift.Player.Controller
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private PlayerEventsNotifier _playerEventsNotifier;

        [SerializeField] private PhysicalMover _physicalMover;
        [SerializeField] private CollisionMover _collisionMover;
        [SerializeField] private PathMover _pathMover;

        private IPlayerState _currentState;

        private void Awake()
        {
            InitPlayerStates();
        }

        private void InitPlayerStates()
        {
            var physicalMovement = new PhysicalMovement(_physicalMover, _playerEventsNotifier);
            var collisionMovement = new CollisionMovement(_collisionMover, _playerEventsNotifier);
            var pathMovement = new PathMovement(_pathMover, _playerEventsNotifier);

            physicalMovement.Init(pathMovement, collisionMovement);
            collisionMovement.Init(physicalMovement);
            pathMovement.Init(physicalMovement);

            OnStateChanged(physicalMovement);
        }

        private void OnStateChanged(IPlayerState newState)
        {
            if (_currentState != null)
            {
                _currentState.StateChanged -= OnStateChanged;
                _currentState.ResetState();
            }
            
            Debug.Log($"PlayerController.OnStateChanged> PLAYER: {_currentState?.GetType().Name} --> {newState.GetType().Name}");


            newState.StateChanged += OnStateChanged;
            newState.PrepareState();

            _currentState = newState;
        }

        public void UpdateController()
        {
            _currentState.UpdateState();
        }
    }
}