using System;
using Plugins.Extensions.OSA.Scripts.DataHelpers;
using Plugins.Extensions.UniRxUtils;
using UI.MainMenu.Leaderboard.Views;
using UniRx;
using Utils.OSA.Grid;

namespace UI.MainMenu.Leaderboard.OSA
{
    public class LeaderboardOsaCollection : LinkedGridAdapter<LeaderboardNodeItemView, LeaderboardOsaParams,
        LeaderboardGridConfig, LeaderboardViewsHolder>
    {
        public SimpleDataHelper<LeaderboardEntity> Data { get; private set; }
        
        public event Action<LeaderboardEntity, LeaderboardNodeItemView> UpdatedItem;
        public event Action<LeaderboardNodeItemView> CreatedItem;
        
        public bool freezeContentEndEdgeOnCountChange;
        
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
        
        public LeaderboardNodeItemView GetItemView(LeaderboardEntity entity)
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
        
        protected override void Start()
        {
            Data = new SimpleDataHelper<LeaderboardEntity>(this);
            base.Start();
        }
        
        protected override void CreateCellViewsHolder(LeaderboardNodeItemView itemNodeItemView)
        {
            CreatedItem?.Invoke(itemNodeItemView);
        }

        protected override void UpdateCellViewsHolder(LeaderboardViewsHolder viewsHolder)
        {
            var model = Data[viewsHolder.ItemIndex];
            UpdatedItem?.Invoke(model, viewsHolder.ItemView);
        }
    }
}