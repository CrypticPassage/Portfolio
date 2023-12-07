using JCMG.EntitasRedux;

namespace Ecs.Avatar.Components
{
    [Avatar, Unique, Event(EventTarget.Self), Event(EventTarget.Self, EventType.Removed)]
    public class CurrentComponent : IComponent
    {
        
    }
}