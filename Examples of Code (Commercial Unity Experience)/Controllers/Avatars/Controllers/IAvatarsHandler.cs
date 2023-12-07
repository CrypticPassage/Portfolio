using System.Collections.Generic;
using UI.MainMenu.Avatars.Views;

namespace UI.MainMenu.Avatars.Controllers
{
    public interface IAvatarsHandler
    {
        void Initialize(InventoryAvatarsView view);
        void Dispose();
        void UpdateAvatarCollection(List<AvatarEntity> entities);
    }
}