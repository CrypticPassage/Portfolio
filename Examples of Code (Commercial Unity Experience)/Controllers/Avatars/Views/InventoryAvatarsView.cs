using SimpleUi.Abstracts;
using UI.MainMenu.Avatars.Osa;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.MainMenu.Avatars.Views
{
    public class InventoryAvatarsView : UiView
    {
        [FormerlySerializedAs("inventoryAvatarsOsaCollection")] [SerializeField] private AvatarsOsaCollection avatarsOsaCollection;

        public AvatarsOsaCollection AvatarsOsaCollection => avatarsOsaCollection;
    }
}