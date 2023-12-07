using JCMG.EntitasRedux;

namespace Ecs.Leaderboard.Components
{
    [Leaderboard]
    public class RatingComponent : IComponent
    {
        public int Value;
    }
}