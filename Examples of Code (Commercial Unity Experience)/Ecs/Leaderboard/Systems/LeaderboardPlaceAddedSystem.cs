using System.Collections.Generic;
using Ecs.Utils.Systems;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Leaderboard.Systems
{
    [Install(ExecutionType.MainMenu, ExecutionPriority.Normal, 600)]
    public class LeaderboardPlaceAddedSystem : ReactiveSystemPooled<LeaderboardEntity>
    {
        private readonly ILeaderboardController _leaderboardController;

        public LeaderboardPlaceAddedSystem(LeaderboardContext leaderboardContext,
            ILeaderboardController leaderboardController
        ) : base(leaderboardContext)
        {
            _leaderboardController = leaderboardController;
        }

        protected override ICollector<LeaderboardEntity> GetTrigger(IContext<LeaderboardEntity> context)
            => context.CreateCollector(LeaderboardMatcher.Place.Added());

        protected override bool Filter(LeaderboardEntity entity)
            => !entity.IsDestroyed; 

        protected override void Execute(List<LeaderboardEntity> entities)
        {
            _leaderboardController.ResetOsa();
        }
    }
}