using JellyShift.JellyInput;
using UnityEngine;

namespace JellyShift.Player.Character
{
    public class JellyCube : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;

        [SerializeField] private TouchInput _input;
        [SerializeField] private Transform _cubeTransform;

        private float _currentOffset;
        private float _lastOffset;

        private void OnEnable()
        {
            _input.OffsetChanged += OnOffsetChanged;
            _input.InputEnded += OnInputEnded;
        }

        private void OnDisable()
        {
            _input.OffsetChanged -= OnOffsetChanged;
            _input.InputEnded -= OnInputEnded;
        }

        private void OnOffsetChanged(float inputOffset)
        {
            var maxOffset = _gameSettings.MaxInputOffset;

            _currentOffset = Mathf.Clamp(inputOffset + _lastOffset, -maxOffset, maxOffset);
            var factor = Remap(_currentOffset, -maxOffset, maxOffset, 0, 1);

            var minDimension = _gameSettings.MinCubeDimension;
            var maxDimension = _gameSettings.MaxCubeDimension;
            var cubeDepth = _gameSettings.CubeDepth;

            var highestState = new Vector3(minDimension, maxDimension, cubeDepth);
            var lowestState = new Vector3(maxDimension, minDimension, cubeDepth);

            var currentScale = Vector3.Lerp(lowestState, highestState, factor);
            _cubeTransform.localScale = currentScale;
            _cubeTransform.localPosition = new Vector3(0, currentScale.y * 0.5f, 0);
        }

        private void OnInputEnded()
        {
            _lastOffset = _currentOffset;
        }
        
        private static float Remap(float inValue, float inMin, float inMax, float outMin, float outMax)
        {
            return outMin + (outMax - outMin) * ((inValue - inMin) / (inMax - inMin));
        }
    }
}