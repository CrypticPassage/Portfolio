using JCMG.EntitasRedux;

namespace Ecs.Leaderboard.Components.Flags
{
    [Leaderboard, Unique, Event(EventTarget.Self), Event(EventTarget.Self, EventType.Removed)]
    public class LeaderboardCurrentComponent : IComponent
    {
        
    }
}