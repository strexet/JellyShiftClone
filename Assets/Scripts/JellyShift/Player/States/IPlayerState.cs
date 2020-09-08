namespace JellyShift.Player.States
{
    public interface IPlayerState
    {
        event PlayerStateDelegate StateChanged;

        void PrepareState();
        void ResetState();
        void UpdateState();
    }
}