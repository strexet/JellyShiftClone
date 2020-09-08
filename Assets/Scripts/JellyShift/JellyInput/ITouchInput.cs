namespace JellyShift.JellyInput
{
    public interface ITouchInput
    {
        event InputChangedDelegate InputStarted;
        event OffsetChangedDelegate OffsetChanged;
        event InputChangedDelegate InputEnded;
        void Enable();
        void Disable();
    }
}