using System.Collections.Generic;
using Ecs.Avatar.Utils;
using Ecs.Utils.Systems;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using UI.Battle.Player.Controllers;
using UI.MainMenu.Avatars.Controllers;
using UI.MainMenu.BattleSearch.Controllers;
using UI.MainMenu.Hero.Controllers;
using UI.MainMenu.Menu.Controllers;
using UI.MainMenu.ProfileSettings.Controllers.Handlers;

namespace Ecs.Avatar.Systems
{
    [Install(ExecutionType.MainMenu, ExecutionPriority.Normal, 600)]
    public class AvatarCurrentAddedSystem : ReactiveSystemPooled<AvatarEntity>, IInitializeSystem
    {
        private readonly AvatarContext _avatarContext;
        private readonly IAvatarSettingsController _avatarSettingsController;
        private readonly ITopHudController _topHudController;
        private readonly IProfileTabHandler _profileTabHandler;

        public AvatarCurrentAddedSystem(AvatarContext avatarContext,
            IAvatarSettingsController avatarSettingsController,
            ITopHudController topHudController,
            IBattleSearchController battleSearchController,
            IProfileTabHandler profileTabHandler
        ) : base(avatarContext)
        {
            _avatarContext = avatarContext;
            _avatarSettingsController = avatarSettingsController;
            _topHudController = topHudController;
            _profileTabHandler = profileTabHandler;
        }
        
        public void Initialize()
        {
            var currentAvatarEntity = _avatarContext.CurrentEntity;
            Execute(new List<AvatarEntity> {currentAvatarEntity});
        }

        protected override ICollector<AvatarEntity> GetTrigger(IContext<AvatarEntity> context)
            => context.CreateCollector(AvatarMatcher.Current.Added());

        protected override bool Filter(AvatarEntity entity)
            => !entity.IsDestroyed;

        protected override void Execute(List<AvatarEntity> entities)
        {
            foreach (var entity in entities)
            {
                _avatarSettingsController.SetAvatar(entity.AvatarId.Value, entity.IsCurrent);
                _topHudController.SetAvatar(entity.AvatarId.Value);
                _profileTabHandler.SetAvatar(entity.AvatarId.Value);
            }
        }
    }
}