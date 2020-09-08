namespace JellyShift.Player.Animation
{
    public interface IPlayerAnimator
    {
        event AnimationDelegate FinishedAnimation;

        void Init();
        void PlayStartAnimation();
        void PlayFinishAnimation();
    }
}