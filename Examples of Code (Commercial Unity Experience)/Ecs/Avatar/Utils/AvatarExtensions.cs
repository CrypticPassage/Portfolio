namespace Ecs.Avatar.Utils
{
    public static class AvatarExtensions
    {
        public static AvatarEntity CreateAvatar(this AvatarContext context, int id, bool isAvailable)
        {
            var avatarEntity = context.CreateEntity();
            avatarEntity.AddAvatarId(id);
            avatarEntity.IsAvailable = isAvailable;
            
            return avatarEntity;
        }

        public static void SetSelectedAvatar(this AvatarContext context, AvatarEntity entity)
        {
            context.SelectedAvatarEntity.IsSelectedAvatar = false;
            entity.IsSelectedAvatar = true;
        }
    }
}