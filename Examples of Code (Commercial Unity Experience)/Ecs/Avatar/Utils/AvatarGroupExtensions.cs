using JCMG.EntitasRedux;

namespace Ecs.Avatar.Utils
{
    public static class AvatarGroupExtensions
    {
        public static IGroup<AvatarEntity> GetAllAvatarGroup(this AvatarContext context)
            => context.GetGroup(AvatarMatcher
                .AllOf(AvatarMatcher.AvatarId)
                .NoneOf(AvatarMatcher.Destroyed)
            );

        public static IGroup<AvatarEntity> GetAvailableAvatarGroup(this AvatarContext context)
            => context.GetGroup(AvatarMatcher
                .AllOf(AvatarMatcher.AvatarId, AvatarMatcher.Available)
                .NoneOf(AvatarMatcher.Destroyed)
            );
    }
}