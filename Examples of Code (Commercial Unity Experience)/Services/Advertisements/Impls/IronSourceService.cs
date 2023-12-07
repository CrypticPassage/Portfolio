using System;
using Game.Db.Advertisements;
using Game.Enums.Advertisements;
using Game.Signals.Ads;
using UniRx;
using UnityEngine;
using Websockets;
using Zenject;

namespace Game.Services.Advertisements.Impls
{
    public class IronSourceService : IAdsProvider, IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly IWebSockets _webSockets;
        private readonly GameContext _gameContext;
        private readonly IAdvertisementsDatabase _advertisementsDatabase;
        private EAdvertisementType _advertisementType;
        private string _appKey;
        
        private Action _onReward;

        public IronSourceService(SignalBus signalBus,
            IWebSockets webSockets,
            GameContext gameContext,
            IAdvertisementsDatabase advertisementsDatabase)
        {
            _signalBus = signalBus;
            _webSockets = webSockets;
            _gameContext = gameContext;
            _advertisementsDatabase = advertisementsDatabase;            
        }

        public void Initialize()
        {
            _advertisementType = _advertisementsDatabase.AdvertisementType;
            _appKey = _advertisementsDatabase.GetAdsDataByType(_advertisementType).IronSourceAppKey;
            
            IronSourceEvents.onSdkInitializationCompletedEvent += LoadRewardedAd;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += HandleReward;
            IronSourceRewardedVideoEvents.onAdAvailableEvent += OnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent += OnAdUnavailable;
            
            _signalBus.GetStream<SignalAdsData>().Subscribe(OnAdsData).AddTo(_disposable);
            _webSockets.GetAdsData();
        }

        public void Dispose()
        {
            IronSourceEvents.onSdkInitializationCompletedEvent -= LoadRewardedAd;
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= HandleReward;
            IronSourceRewardedVideoEvents.onAdAvailableEvent -= OnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent -= OnAdUnavailable;
        }
        
        public void ShowRewardedAd(Action onReward, Action onError)
        {
            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSource.Agent.showRewardedVideo();
                _onReward = onReward;
            }
            else
            {
                onError?.Invoke();
                LoadRewardedAd();
            }
        }

        private void HandleReward(IronSourcePlacement placement, IronSourceAdInfo info)
        {
            Debug.Log($"[IronSource] OnAdRewardedEvent");
            Debug.Log($"[IronSource] Placement Name: {placement.getPlacementName()}");
            Debug.Log($"[IronSource] A/B Test: {info.ab}");
            Debug.Log($"[IronSource] Country: {info.country}");
            Debug.Log($"[IronSource] Precision: {info.precision}");
            Debug.Log($"[IronSource] Revenue: {info.revenue}");
            Debug.Log($"[IronSource] Ad Network: {info.adNetwork}");
            Debug.Log($"[IronSource] Ad Unit: {info.adUnit}");
            Debug.Log($"[IronSource] Auction ID: {info.auctionId}");
            Debug.Log($"[IronSource] Instance ID: {info.instanceId}");
            Debug.Log($"[IronSource] Instance Name: {info.instanceName}");
            Debug.Log($"[IronSource] Lifetime Revenue: {info.lifetimeRevenue}");
            Debug.Log($"[IronSource] Segment Name: {info.segmentName}");
            Debug.Log($"[IronSource] Encrypted CPM: {info.encryptedCPM}");
            
            OnAdRewarded();
        }

        private void OnAdRewarded()
        {
            _onReward?.Invoke();
            _onReward = null;
            LoadRewardedAd();
        }

        private void OnAdAvailable(IronSourceAdInfo adInfo) => _gameContext.ReplaceAdsReady(true);

        private void OnAdUnavailable() => _gameContext.ReplaceAdsReady(false);

        private void InitializeIronSource() => IronSource.Agent.init(_appKey);

        private void LoadRewardedAd() => IronSource.Agent.loadRewardedVideo();
        
        private void OnAdsData(SignalAdsData signal)
        {
            var adsData = signal.Value;

           if (adsData.isAdsActive)
           {
               _gameContext.ReplaceAdsActive(adsData.isAdsActive);
               InitializeIronSource();
           }
        }
    }
}