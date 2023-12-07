using System;
using System.Collections.Generic;
using Ecs.Avatar.Utils;
using Services.Sorting;
using UI.MainMenu.Avatars.Databases;
using UI.MainMenu.Avatars.Views;
using UniRx;
using Zenject;

namespace UI.MainMenu.Avatars.Controllers.Impls
{
	public class AvatarsHandler : IAvatarsHandler
	{
		protected readonly SignalBus SignalBus;
		protected readonly AvatarContext _avatarContext;
		private readonly IAvatarDatabase _avatarDatabase;
		private readonly ISortingService _sortingService;
		protected InventoryAvatarsView View;
		private IDisposable _osaInitializeDisposable = UniRx.Disposable.Empty;

		public AvatarsHandler(
			SignalBus signalBus,
			AvatarContext avatarContext,
			IAvatarDatabase avatarDatabase,
			ISortingService sortingService
		)
		{
			SignalBus = signalBus;
			_avatarContext = avatarContext;
			_avatarDatabase = avatarDatabase;
			_sortingService = sortingService;
		}

		public virtual void Initialize(InventoryAvatarsView view)
		{
			View = view;

			View.AvatarsOsaCollection.CreatedItem += InventoryAvatarsOsaCollectionOnCreateItem;
			View.AvatarsOsaCollection.UpdatedItem += InventoryAvatarsOsaCollectionOnUpdatedItem;
		}

		public void Dispose()
		{
			_osaInitializeDisposable.Dispose();

			View.AvatarsOsaCollection.CreatedItem -= InventoryAvatarsOsaCollectionOnCreateItem;
			View.AvatarsOsaCollection.UpdatedItem -= InventoryAvatarsOsaCollectionOnUpdatedItem;
		}

		public void SelectAvatar(AvatarItemView itemView)
		{
			var entity = _avatarContext.GetEntityWithAvatarId(itemView.AvatarId);

			if (entity.IsCurrent)
				return;
			
			if (_avatarContext.IsSelectedAvatar)
			{
				if (entity == _avatarContext.SelectedAvatarEntity) 
					_avatarContext.SelectedAvatarEntity.IsSelectedAvatar = false;
				else
					_avatarContext.SetSelectedAvatar(entity);
			}
			else
				entity.IsSelectedAvatar = true;
		}
		
		public void UpdateAvatarCollection(List<AvatarEntity> entities)
		{
			var collection = View.AvatarsOsaCollection; 
			collection.SubscribeOnInitialize(() => collection.Data.ResetItems(entities));
		}

		protected virtual void InventoryAvatarsOsaCollectionOnCreateItem(AvatarItemView itemView)
		{ 
			itemView.ItemButton.OnClickAsObservable().Subscribe(_ => SelectAvatar(itemView)).AddTo(itemView);
		}

		protected virtual void InventoryAvatarsOsaCollectionOnUpdatedItem(AvatarEntity entity, AvatarItemView itemView)
		{
			itemView.Unlink();
			var icon = _avatarDatabase.GetAvatarById(entity.AvatarId.Value).Sprite;
			var color = _avatarDatabase.GetStyleByType(entity.IsAvailable).FrameColor;
			itemView.SetAvatar(icon, color, entity.IsAvailable);
			itemView.Link(entity, _avatarContext);
		}
	}
}