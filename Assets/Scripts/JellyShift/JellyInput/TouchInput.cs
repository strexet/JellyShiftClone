using UnityEngine;

namespace JellyShift.JellyInput
{
    public class TouchInput : MonoBehaviour, ITouchInput
    {
        private const float Tolerance = 0.01f;

        private Vector2 _touchStartPosition;
        private Vector2 _touchEndPosition;

        private bool _isDisabled;

        public event InputChangedDelegate InputStarted = delegate { };
        public event OffsetChangedDelegate OffsetChanged = delegate { };
        public event InputChangedDelegate InputEnded = delegate { };

        public void Enable()
        {
            _isDisabled = false;
        }

        public void Disable()
        {
            _isDisabled = true;
        }

        private void Update()
        {
            if (_isDisabled) return;

#if UNITY_EDITOR
            UpdateClickPosition();
#else
            UpdateTouchPosition();
#endif
        }

        private void UpdateClickPosition()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _touchStartPosition = Input.mousePosition;
                InputStarted.Invoke();
                return;
            }

            if (Input.GetMouseButton(0))
            {
                OnTouchMove(Input.mousePosition);
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnTouchMove(Input.mousePosition);
                InputEnded.Invoke();
            }
        }

        private void UpdateTouchPosition()
        {
            if (Input.touchCount == 0) return;

            var touch = Input.GetTouch(0);
            var touchPhase = touch.phase;
            var touchPosition = touch.position;

            if (touchPhase == TouchPhase.Began)
            {
                _touchStartPosition = touchPosition;
                InputStarted.Invoke();
                return;
            }

            if (touchPhase == TouchPhase.Moved)
            {
                OnTouchMove(touchPosition);
                return;
            }

            if (touchPhase == TouchPhase.Ended)
            {
                OnTouchMove(touchPosition);
                InputEnded.Invoke();
            }
        }

        private void OnTouchMove(Vector2 touchPosition)
        {
            if (Mathf.Abs(_touchEndPosition.y - touchPosition.y) < Tolerance) return;

            _touchEndPosition = touchPosition;
            UpdateVerticalOffset();
        }

        private void UpdateVerticalOffset()
        {
            var yDiff = _touchEndPosition.y - _touchStartPosition.y;

            if (Mathf.Abs(yDiff) > Tolerance)
            {
                OffsetChanged.Invoke(yDiff);
            }
        }
    }
}