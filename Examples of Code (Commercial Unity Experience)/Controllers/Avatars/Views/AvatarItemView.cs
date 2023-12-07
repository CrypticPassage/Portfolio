using Ecs.Utils.View.Impls;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils.OSA.Grid.Interfaces;

namespace UI.MainMenu.Avatars.Views
{
    public class AvatarItemView : LinkedUiView<AvatarEntity>, ICurrentAddedListener, ICurrentRemovedListener,
        ISelectedAvatarAddedListener, ISelectedAvatarRemovedListener, IAvailableAddedListener, IAvailableRemovedListener,
        IGridItemView
    {
        [SerializeField] private Image avatarIcon;
        [SerializeField] private Image lockedFade;
        [SerializeField] private Image locked;
        [SerializeField] private Image defaultFrame;
        [SerializeField] private Image selectedFrame;
        [SerializeField] private Image currentFrame;
        [SerializeField] private Image checkmarkicon;
        [SerializeField] private Button itemButton;
        [SerializeField] private GameObject content;
        [SerializeField] private LayoutElement layoutElement;
        
        public GameObject Content => content;
        public LayoutElement LayoutElement => layoutElement;
        public Button ItemButton => itemButton;
        
        private int _avatarId;

        public int AvatarId => _avatarId;

        protected override void Listen(AvatarEntity entity)
        {
            _avatarId = entity.AvatarId.Value;
            
            entity.AddCurrentAddedListener(this);
            entity.AddCurrentRemovedListener(this);
            entity.AddSelectedAvatarAddedListener(this);
            entity.AddSelectedAvatarRemovedListener(this);
            entity.AddAvailableAddedListener(this);
            entity.AddAvailableRemovedListener(this);

            if (entity.IsCurrent)
                OnCurrentAdded(entity);
            else
                OnCurrentRemoved(entity);
            
            if(entity.IsSelectedAvatar)
                OnSelectedAvatarAdded(entity);
            else
                OnSelectedAvatarRemoved(entity);
            
            if(entity.IsAvailable)
                OnAvailableAdded(entity);
            else
                OnAvailableRemoved(entity);
        }

        protected override void Unlisten(AvatarEntity entity)
        {
            entity.RemoveCurrentAddedListener(this);
            entity.RemoveCurrentRemovedListener(this);
            entity.RemoveSelectedAvatarAddedListener(this);
            entity.RemoveSelectedAvatarRemovedListener(this);
            entity.RemoveAvailableAddedListener(this);
            entity.RemoveAvailableRemovedListener(this);
        }

        protected override void InternalClear() { }

        public void SetAvatar(Sprite avatarSprite, Color selectedColor, bool isAvailable)
        {
            avatarIcon.sprite = avatarSprite;
            selectedFrame.color = selectedColor;
            lockedFade.enabled = !isAvailable;
        }
        
        public void OnCurrentAdded(AvatarEntity entity)
        {
            currentFrame.enabled = true;
            checkmarkicon.enabled = true;
            selectedFrame.enabled = false;
        }

        public void OnCurrentRemoved(AvatarEntity entity)
        {
            currentFrame.enabled = false;
            checkmarkicon.enabled = false;
        }

        public void OnSelectedAvatarAdded(AvatarEntity entity)
        {
            selectedFrame.enabled = true;
        }

        public void OnSelectedAvatarRemoved(AvatarEntity entity)
        {
            selectedFrame.enabled = false;
        }

        public void OnAvailableAdded(AvatarEntity entity)
        {
            locked.enabled = false;
        }

        public void OnAvailableRemoved(AvatarEntity entity)
        {
            locked.enabled = true;
        }
    }
}