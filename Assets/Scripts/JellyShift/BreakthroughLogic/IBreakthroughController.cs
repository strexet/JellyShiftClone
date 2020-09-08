namespace JellyShift.BreakthroughLogic
{
    public interface IBreakthroughController
    {
        event BreakthroughDelegate BreakthroughStarted;
        event BreakthroughDelegate BreakthroughFinished;

        void UpdateController();
        void StopBreakthrough();
    }
}