using System;
using Core.Network.Models.Api.Dtos.Avatar;
using Services.Network;
using Zenject;

namespace Services.Avatar.Imps
{
    public class AvatarService : IAvatarService, IInitializable, IDisposable
    {
        private readonly INetworkCallbacks _networkCallbacks;
        private readonly INetworkRequests _networkRequests;
        private readonly AvatarContext _avatarContext;

        public AvatarService(
            INetworkCallbacks networkCallbacks,
            INetworkRequests networkRequests,
            AvatarContext avatarContext
        )
        {
            _networkCallbacks = networkCallbacks;
            _networkRequests = networkRequests;
            _avatarContext = avatarContext;
        }
        
        public void Initialize()
        {
            _networkCallbacks.OnChangeAvatar += OnChangeAvatar;
        }
        
        public void Dispose()
        {
            _networkCallbacks.OnChangeAvatar -= OnChangeAvatar;
        }

        public void SendAvatarRequest(int id) => _networkRequests.ChangeAvatar(id);

        private void OnChangeAvatar(AvatarDto message)
        {
            var entity = _avatarContext.GetEntityWithAvatarId(message.currentId);
            
            _avatarContext.CurrentEntity.IsCurrent = false; 
            entity.IsCurrent = true;
        }
    }
}