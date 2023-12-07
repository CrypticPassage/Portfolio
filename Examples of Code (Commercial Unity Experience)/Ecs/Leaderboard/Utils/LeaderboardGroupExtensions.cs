using JCMG.EntitasRedux;

namespace Ecs.Leaderboard.Utils
{
    public static class LeaderboardGroupExtensions
    {
        public static IGroup<LeaderboardEntity> GetAllDisplayedGroup(this LeaderboardContext context)
            => context.GetGroup(LeaderboardMatcher
                .AllOf(LeaderboardMatcher.Displayed)
                .NoneOf(LeaderboardMatcher.Destroyed)
            );
    }
}