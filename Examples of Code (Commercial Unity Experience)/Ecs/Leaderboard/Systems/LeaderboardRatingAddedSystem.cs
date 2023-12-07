using System.Collections.Generic;
using Ecs.Utils.Systems;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using Services.Leaderboard;
using Services.UserProfile;

namespace Ecs.Leaderboard.Systems
{
    [Install(ExecutionType.MainMenu, ExecutionPriority.Normal, 500)]
    public class LeaderboardRatingAddedSystem : ReactiveSystemPooled<LeaderboardEntity>
    {
        private readonly LeaderboardContext _leaderboardContext;
        private readonly IUserProfileService _userProfileService;

        public LeaderboardRatingAddedSystem(LeaderboardContext leaderboardContext,
            IUserProfileService userProfileService
        ) : base(leaderboardContext)
        {
            _leaderboardContext = leaderboardContext;
            _userProfileService = userProfileService;
        }

        protected override ICollector<LeaderboardEntity> GetTrigger(IContext<LeaderboardEntity> context)
            => context.CreateCollector(LeaderboardMatcher.Rating.Added());

        protected override bool Filter(LeaderboardEntity entity)
            => !entity.IsDestroyed; 

        protected override void Execute(List<LeaderboardEntity> entities)
        {
            foreach (var entity in entities)
            {
                var rating = entity.Rating.Value;
                var rankScore = _userProfileService.GetRankScore(rating); 
                entity.ReplaceRankScore(rankScore);
            }
        }
    }
}