using System.Collections.Generic;
using Ecs.Avatar.Utils;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using UI.MainMenu.Avatars.Controllers;

namespace Ecs.Avatar.Systems
{
    [Install(ExecutionType.MainMenu, ExecutionPriority.Normal, 500)]
    public class AvatarInitializeSystem : IInitializeSystem
    {
        private readonly AvatarContext _avatarContext;
        private readonly IAvatarsHandler _avatarController;
        private readonly IGroup<AvatarEntity> _avatarGroup;
        private readonly List<AvatarEntity> _avatarBuffer = new List<AvatarEntity>();

        public AvatarInitializeSystem(AvatarContext avatarContext,
            IAvatarsHandler avatarController
        )
        {
            _avatarContext = avatarContext;
            _avatarController = avatarController;
            _avatarGroup = avatarContext.GetAllAvatarGroup();
        }

        public void Initialize()
        { 
            _avatarGroup.GetEntities(_avatarBuffer);
            _avatarController.UpdateAvatarCollection(_avatarBuffer);
        }
    }
}