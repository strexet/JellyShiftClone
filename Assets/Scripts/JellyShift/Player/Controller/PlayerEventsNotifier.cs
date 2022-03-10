using System.Collections;
using System.Collections.Generic;
using JellyShift.Game.Physics;
using JellyShift.Level.Zones;
using JellyShift.Player.Movement;
using UnityEngine;

namespace JellyShift.Player.Controller
{
    public class PlayerEventsNotifier : MonoBehaviour, IPlayerEventsNotifier
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private CollisionDetector _collisionDetector;

        [Header("Movers")]
        [SerializeField] private PathMover _pathMover;
        [SerializeField] private PhysicalMover _physicalMover;
        [SerializeField] private CollisionMover _collisionMover;

        [Header("Zones")]
        [SerializeField] private FinishLevelZone _finishLevelZone;
        [SerializeField] private List<LoseLevelZone> _loseLevelZones;
        [SerializeField] private List<StartTransitionZone> _startTransitionZones;
        [SerializeField] private List<FinishTransitionZone> _finishTransitionZones;

        private bool _isColliding;

        public event PlayerEventDelegate EnteredLoseZone = delegate { };
        public event PlayerEventDelegate EnteredFinishZone = delegate { };
        public event PlayerEventDelegate EnteredTransitionZone = delegate { };
        public event PlayerEventDelegate EscapedTransitionZone = delegate { };
        public event PlayerEventDelegate CollisionStarted = delegate { };
        public event PlayerEventDelegate CollisionFinished = delegate { };

        private void OnEnable()
        {
            _collisionDetector.Collided += OnCollision;
            
            _finishLevelZone.EnteredZone += OnEnteredFinishZone;

            foreach (var loseLevelZone in _loseLevelZones)
            {
                loseLevelZone.EnteredZone += OnEnteredLoseZone;
            }

            foreach (var startTransitionZone in _startTransitionZones)
            {
                startTransitionZone.EnteredZone += OnEnteredTransitionZone;
            }

            foreach (var finishTransitionZone in _finishTransitionZones)
            {
                finishTransitionZone.EnteredZone += OnEscapedTransitionZone;
            }
        }

        private void OnDisable()
        {
            _collisionDetector.Collided -= OnCollision;
            
            _finishLevelZone.EnteredZone -= OnEnteredFinishZone;

            foreach (var loseLevelZone in _loseLevelZones)
            {
                loseLevelZone.EnteredZone -= OnEnteredLoseZone;
            }

            foreach (var startTransitionZone in _startTransitionZones)
            {
                startTransitionZone.EnteredZone -= OnEnteredTransitionZone;
            }

            foreach (var finishTransitionZone in _finishTransitionZones)
            {
                finishTransitionZone.EnteredZone -= OnEscapedTransitionZone;
            }
        }

        private void OnCollision(GameObject other)
        {
            if (other.layer != _gameSettings.ObstacleLayerIndex
                || _isColliding)
            {
                return;
            }

            StartCoroutine(CollisionRoutine());
        }

        private IEnumerator CollisionRoutine()
        {
            _isColliding = true;
            CollisionStarted.Invoke();

            yield return new WaitForSeconds(_gameSettings.CollisionStopDuration);

            CollisionFinished.Invoke();
            _isColliding = false;
        }

        private void OnEnteredFinishZone(TriggerZone zone)
        {
            EnteredFinishZone.Invoke();
        }

        private void OnEnteredLoseZone(TriggerZone zone)
        {
            EnteredLoseZone.Invoke();
        }

        private void OnEnteredTransitionZone(TriggerZone zone)
        {
            if (zone is StartTransitionZone transitionZone)
            {
                _pathMover.Setup(transitionZone.Path, transitionZone.Speed);
            }

            EnteredTransitionZone.Invoke();
        }

        private void OnEscapedTransitionZone(TriggerZone zone)
        {
            if (zone is FinishTransitionZone transitionZone)
            {
                var moveDirection = transitionZone.MoveDirectionAfterTransition;
                _physicalMover.Setup(moveDirection);
                _collisionMover.Setup(moveDirection);
            }

            EscapedTransitionZone.Invoke();
        }

        private void OnValidate()
        {
            _finishLevelZone = FindObjectOfType<FinishLevelZone>();

            var loseLevelZones = FindObjectsOfType<LoseLevelZone>();
            _loseLevelZones = new List<LoseLevelZone>();
            _loseLevelZones.AddRange(loseLevelZones);

            var startTransitionZones = FindObjectsOfType<StartTransitionZone>();
            _startTransitionZones = new List<StartTransitionZone>();
            _startTransitionZones.AddRange(startTransitionZones);

            var finishTransitionZones = FindObjectsOfType<FinishTransitionZone>();
            _finishTransitionZones = new List<FinishTransitionZone>();
            _finishTransitionZones.AddRange(finishTransitionZones);
        }
    }
}