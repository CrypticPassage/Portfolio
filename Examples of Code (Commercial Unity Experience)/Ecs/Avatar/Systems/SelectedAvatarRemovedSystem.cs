using System.Collections.Generic;
using Ecs.Avatar.Utils;
using Ecs.Utils.Systems;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using UI.MainMenu.Avatars.Controllers;
using UI.MainMenu.Avatars.Databases;
using UI.MainMenu.Hero.Controllers;
using UI.MainMenu.Menu.Controllers;
using UI.MainMenu.ProfileSettings.Controllers.Handlers;

namespace Ecs.Avatar.Systems
{
    [Install(ExecutionType.MainMenu, ExecutionPriority.Normal, 605)]
    public class SelectedAvatarRemovedSystem : ReactiveSystemPooled<AvatarEntity>
    {
        private readonly AvatarContext _avatarContext;
        private readonly IAvatarSettingsController _avatarSettingsController;

        public SelectedAvatarRemovedSystem(AvatarContext avatarContext,
            IAvatarSettingsController avatarSettingsController
        ) : base(avatarContext)
        {
            _avatarContext = avatarContext;
            _avatarSettingsController = avatarSettingsController;
        }

        protected override ICollector<AvatarEntity> GetTrigger(IContext<AvatarEntity> context)
            => context.CreateCollector(AvatarMatcher.SelectedAvatar.Removed());

        protected override bool Filter(AvatarEntity entity)
            => !entity.IsDestroyed;

        protected override void Execute(List<AvatarEntity> entities)
        {
            _avatarSettingsController.SetAvatar(_avatarContext.CurrentEntity.AvatarId.Value, _avatarContext.CurrentEntity.IsAvailable);
        }
    }
}