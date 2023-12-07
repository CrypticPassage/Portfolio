using JCMG.EntitasRedux;

namespace Ecs.Leaderboard.Components.Flags
{
    [Leaderboard, Event(EventTarget.Self), Event(EventTarget.Self, EventType.Removed)]
    public class TopPlaceComponent : IComponent
    {
        
    }
}