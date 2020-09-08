namespace JellyShift.Player.Controller
{
    public interface IPlayerEventsNotifier
    {
        event PlayerEventDelegate EnteredFinishZone;
        event PlayerEventDelegate EnteredTransitionZone;
        event PlayerEventDelegate EscapedTransitionZone;
        event PlayerEventDelegate CollisionStarted;
        event PlayerEventDelegate CollisionFinished;
        event PlayerEventDelegate EnteredLoseZone;
    }
}