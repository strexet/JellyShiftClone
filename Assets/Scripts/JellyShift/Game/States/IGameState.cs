namespace JellyShift.Game.States
{
    public interface IGameState
    {
        event GameStateDelegate StateChanged;

        void PrepareState();
        void ResetState();
        void UpdateState();
    }
}