using System.Collections.Generic;
using UnityEngine;

namespace JellyShift.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private List<ObstaclePart> _parts;

        public event ObstacleEventDelegate PlayerPassed = delegate { };
        public event ObstacleEventDelegate PlayerContacted = delegate { };
        public event ObstacleEventDelegate PlayerCollided = delegate { };

        private void OnDisable()
        {
            PlayerPassed = delegate { };
            PlayerContacted = delegate { };
            PlayerCollided = delegate { };
        }

        public void OnChildCollision()
        {
            PlayerCollided.Invoke();
        }

        public void SwapMaterials()
        {
            foreach (var part in _parts)
            {
                part.SwapMaterials();
            }
        }

        public void Break()
        {
            var position = transform.position;

            foreach (var part in _parts)
            {
                part.AddForceFrom(position);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerContacted.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            PlayerPassed.Invoke();
        }

        private void OnValidate()
        {
            var foundParts = transform.GetComponentsInChildren<ObstaclePart>();
            _parts = new List<ObstaclePart>();
            _parts.AddRange(foundParts);
        }
    }
}