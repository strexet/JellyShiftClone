using System.Collections.Generic;
using UnityEngine;

namespace JellyShift.BreakthroughLogic
{
    public class BreakthroughBehaviourManager : MonoBehaviour
    {
        [SerializeField] private BreakthroughController _breakthroughController;
        [SerializeField] private List<BreakthroughObstacleBehaviour> _obstacleBehaviours;

        private void OnEnable()
        {
            _breakthroughController.BreakthroughStarted += StartBreakthrough;
            _breakthroughController.BreakthroughFinished += StopBreakthrough;
        }

        private void OnDisable()
        {
            _breakthroughController.BreakthroughStarted -= StartBreakthrough;
            _breakthroughController.BreakthroughFinished -= StopBreakthrough;
        }

        private void StartBreakthrough()
        {
            foreach (var breakthroughBehaviour in _obstacleBehaviours)
            {
                breakthroughBehaviour.StartBreakthrough();
            }
        }

        private void StopBreakthrough()
        {
            foreach (var breakthroughBehaviour in _obstacleBehaviours)
            {
                breakthroughBehaviour.StopBreakthrough();
            }
        }

        private void OnValidate()
        {
            var found = FindObjectsOfType<BreakthroughObstacleBehaviour>();
            _obstacleBehaviours = new List<BreakthroughObstacleBehaviour>();
            _obstacleBehaviours.AddRange(found);
        }
    }
}