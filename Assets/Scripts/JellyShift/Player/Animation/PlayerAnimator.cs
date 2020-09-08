using System.Collections;
using UnityEngine;

namespace JellyShift.Player.Animation
{
    public class PlayerAnimator : MonoBehaviour, IPlayerAnimator
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _startAnimationDuration = 0.55f;
        [SerializeField] private float _finishAnimationDuration = 1.35f;

        private bool _isAnimating;

        private const string EntryTriggerName = "Entry";
        private const string StartTriggerName = "Start";
        private const string FinishTriggerName = "Finish";

        private readonly int _entryTrigger = Animator.StringToHash(EntryTriggerName);
        private readonly int _startTrigger = Animator.StringToHash(StartTriggerName);
        private readonly int _finishTrigger = Animator.StringToHash(FinishTriggerName);

        public event AnimationDelegate FinishedAnimation = delegate { };

        public void Init()
        {
            _animator.SetTrigger(_entryTrigger);
        }

        public void PlayStartAnimation()
        {
            if (_isAnimating) return;

            _isAnimating = true;

            _animator.SetTrigger(_startTrigger);
            StartCoroutine(Wait(_startAnimationDuration));
        }

        public void PlayFinishAnimation()
        {
            if (_isAnimating) return;

            _isAnimating = true;

            _animator.SetTrigger(_finishTrigger);
            StartCoroutine(Wait(_finishAnimationDuration));
        }

        private IEnumerator Wait(float animationTime)
        {
            yield return new WaitForSeconds(animationTime);
            _isAnimating = false;

            FinishedAnimation.Invoke();
        }
    }
}