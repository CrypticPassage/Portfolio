using System;
using Plugins.Extensions.OSA.Scripts.DataHelpers;
using Plugins.Extensions.UniRxUtils;
using UI.MainMenu.Avatars.Views;
using UniRx;
using Utils.OSA.Grid;

namespace UI.MainMenu.Avatars.Osa
{
   public class AvatarsOsaCollection : LinkedGridAdapter<AvatarItemView, AvatarsOsaParms,
   		AvatarsGridConfig, AvatarItemViewsHolder>
   	{
   		public SimpleDataHelper<AvatarEntity> Data { get; private set; }
           
   		public event Action<AvatarEntity, AvatarItemView> UpdatedItem;
   		public event Action<AvatarItemView> CreatedItem;
   
   		public bool freezeContentEndEdgeOnCountChange;
   		
   		protected override void Start()
   		{
   			Data = new SimpleDataHelper<AvatarEntity>(this);
   			base.Start();
   		}
   		
   		public IDisposable SubscribeOnInitialize(System.Action onInitialize)
   		{
   			if (IsInitialized)
   			{
   				onInitialize();
   				return Disposable.Empty;
   			}
   			
   			return Observable.FromEvent(action => Initialized += action, action => Initialized -= action)
   				.ToSingle().Subscribe(_ => onInitialize());
   		}
   
   		public AvatarItemView GetItemView(AvatarEntity entity)
   		{
   			if (Data == null)
   				return null;
   			
   			var itemIndex = Data.List.IndexOf(entity);
   			var viewsHolder = GetCellViewsHolderIfVisible(itemIndex);
   
   			return viewsHolder?.ItemView;
   		}
   		
   		public override void Refresh(bool contentPanelEndEdgeStationary = false /*ignored*/, bool keepVelocity = false)
   		{
   			_CellsCount = Data.Count;
   			base.Refresh(freezeContentEndEdgeOnCountChange, keepVelocity);
   		}
   		protected override void CreateCellViewsHolder(AvatarItemView itemView) => CreatedItem?.Invoke(itemView);
   
   		protected override void UpdateCellViewsHolder(AvatarItemViewsHolder viewsHolder)
   		{
   			var model = Data[viewsHolder.ItemIndex];
   			UpdatedItem?.Invoke(model, viewsHolder.ItemView);
   		}
   	}
}