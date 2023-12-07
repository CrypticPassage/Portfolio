using JCMG.EntitasRedux;

namespace Ecs.Leaderboard.Components
{
    [Leaderboard]
    public class PlaceComponent : IComponent
    {
       [PrimaryEntityIndex] public string Value;
    }
}