using JCMG.EntitasRedux;

namespace Ecs.Avatar.Components
{
    [Avatar]
    public class AvatarIdComponent : IComponent
    {
        [PrimaryEntityIndex] public int Value;
    }
}