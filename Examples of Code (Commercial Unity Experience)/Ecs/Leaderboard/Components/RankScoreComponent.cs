using JCMG.EntitasRedux;

namespace Ecs.Leaderboard.Components
{
    [Leaderboard]
    public class RankScoreComponent : IComponent
    {
        public int Value;
    }
}