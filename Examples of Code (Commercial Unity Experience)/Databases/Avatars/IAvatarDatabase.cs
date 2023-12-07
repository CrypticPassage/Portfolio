using Models.Avatars;
using Models.Skills;
using UI.MainMenu.Avatars.Vo;

namespace UI.MainMenu.Avatars.Databases
{
    public interface IAvatarDatabase
    {
        AvatarVo[] Avatars { get; }
        AvatarVo GetAvatarById(int id);
        AvatarStyleSettingsVo GetStyleByType(bool type);
    }
}