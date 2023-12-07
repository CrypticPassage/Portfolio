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
    [Install(ExecutionType.MainMenu, ExecutionPriority.Normal, 610)]
    public class SelectedAvatarAddedSystem : ReactiveSystemPooled<AvatarEntity>
    {
        private readonly IAvatarSettingsController _avatarSettingsController;

        public SelectedAvatarAddedSystem(AvatarContext avatarContext,
            IAvatarSettingsController avatarSettingsController
        ) : base(avatarContext)
        {
            _avatarSettingsController = avatarSettingsController;
        }

        protected override ICollector<AvatarEntity> GetTrigger(IContext<AvatarEntity> context)
            => context.CreateCollector(AvatarMatcher.SelectedAvatar.Added());

        protected override bool Filter(AvatarEntity entity)
            => !entity.IsDestroyed; 

        protected override void Execute(List<AvatarEntity> entities)
        {
            foreach (var entity in entities)
            {
                _avatarSettingsController.SetAvatar(entity.AvatarId.Value, entity.IsAvailable);
            }
        }
    }
}