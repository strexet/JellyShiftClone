using System;
using PathCreation;
using UnityEngine;

namespace JellyShift.Player.Movement
{
    public class PathMover : MonoBehaviour, IMover
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private EndOfPathInstruction _endOfPathInstruction;

        private GameObject _gameObject;
        private PathCreator _pathCreator;
        private float _pathSpeed;

        private float _distanceTravelled;

        private void Awake()
        {
            _gameObject = gameObject;
        }

        public void Setup(PathCreator transitionPath, float transitionSpeed)
        {
            _pathCreator = transitionPath;
            _pathSpeed = transitionSpeed;
        }

        public void Init()
        {
            _gameObject.layer = _gameSettings.NormalPlayerLayerIndex;
            _rigidbody.isKinematic = true;
            _rigidbody.constraints = RigidbodyConstraints.None;
            MoveToPath();
        }

        public void Move()
        {
            _distanceTravelled += _pathSpeed;
            var position = _pathCreator.path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction);
            var rotation = _pathCreator.path.GetRotationAtDistance(_distanceTravelled, _endOfPathInstruction);

            rotation *= Quaternion.AngleAxis(90, Vector3.forward);

            _rigidbody.MovePosition(position);
            _rigidbody.MoveRotation(rotation);
        }

        private void MoveToPath()
        {
            _distanceTravelled = _pathCreator.path.GetClosestDistanceAlongPath(_rigidbody.position);
        }
    }
}