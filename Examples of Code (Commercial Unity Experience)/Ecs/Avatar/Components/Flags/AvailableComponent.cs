using JCMG.EntitasRedux;

namespace Ecs.Avatar.Components
{
    [Avatar, Event(EventTarget.Self), Event(EventTarget.Self, EventType.Removed)]
    public class AvailableComponent : IComponent
    {
        
    }
}