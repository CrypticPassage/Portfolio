// using System;
// using System.Collections.Generic;
// using Ecs.Game.Components;
// using Game.Db.Advertisements;
// using Game.Enums.Advertisements;
// using Game.Signals.Ads;
// using GoogleMobileAds.Api;
// using Ui.Signals;
// using UniRx;
// using UnityEngine;
// using Websockets;
// using Zenject;
//
// namespace Game.Services.Advertisements.Impls
// {
//     public class AdMobService : IAdMobService, IInitializable
//     {
//         private readonly SignalBus _signalBus;
//         private readonly CompositeDisposable _disposable = new CompositeDisposable();
//         private readonly IWebSockets _webSockets;
//         private readonly IAdvertisementsDatabase _advertisementsDatabase;
//         private readonly GameContext _gameContext;
//         private readonly EAdvertisementType _advertisementType;
//         private readonly string _rewardedAdId;
//         //private RewardedAd _rewardedAd;
//
//         public AdMobService(SignalBus signalBus,
//             IWebSockets webSockets,
//             IAdvertisementsDatabase advertisementsDatabase,
//             GameContext gameContext)
//         {
//             _signalBus = signalBus;
//             _webSockets = webSockets;
//             _advertisementsDatabase = advertisementsDatabase;
//             _gameContext = gameContext;
//             _advertisementType = _advertisementsDatabase.AdvertisementType; 
//             _rewardedAdId = _advertisementsDatabase.GetAdsDataByType(_advertisementType).AdMobRewardedKey;
//         }
//
//         public void Initialize()
//         { 
//             _signalBus.GetStream<SignalAdsData>().Subscribe(OnAdsData).AddTo(_disposable);
//             _webSockets.GetAdsData();
//         }
//
//         public void ShowRewardedAd(Action<int> onReward, Action onError)
//         {
//             if (_rewardedAd == null || !_rewardedAd.CanShowAd())
//             {
//                 MainThreadDispatcher.Post(_ =>
//                 {
//                     onError?.Invoke();
//                     LoadRewardedAd();
//                 }, null);
//             }
//             else
//                 _rewardedAd?.Show(reward =>
//                     MainThreadDispatcher.Post(_ =>
//                     {
//                         onReward?.Invoke(reward);
//                         LoadRewardedAd();
//                     }, null));
//         }
//         
//         private void LoadRewardedAd()
//         {
//             if (_rewardedAd != null)
//             {
//                 _rewardedAd.Destroy();
//                 _rewardedAd = null;
//             }
//             
//             var adRequest = new AdRequest();
//             
//             RewardedAd.Load(_rewardedAdId, adRequest,
//                 (ad, error) =>
//                 {
//                     if (error != null || ad == null)
//                     {
//                         Debug.LogError($"[AdMobService] On ad load error: {error.GetMessage()}");
//                         return;
//                     }
//
//                     _rewardedAd = ad;
//                 });
//         }
//         
//         private void OnAdsData(SignalAdsData signal)
//         { 
//             var adsData = signal.Value;
//
//             if (adsData.isAdsActive)
//             {
//                 _gameContext.ReplaceAdsActive(adsData.isAdsActive);
//                 InitializeAdMob();
//             }
//         }
//
//         private void InitializeAdMob()
//         {
//             MobileAds.Initialize(initStatus =>
//             {
//                 SetRequestConfiguration();
//                 LoadRewardedAd();
//             });
//         }
//
//         private void SetRequestConfiguration()
//         { 
//             var requestConfiguration = new RequestConfiguration()
//             {
//                 TestDeviceIds = new List<string>(_advertisementsDatabase.AdMobTestDevicesIds),
//                 TagForChildDirectedTreatment = TagForChildDirectedTreatment.False,
//                 TagForUnderAgeOfConsent = TagForUnderAgeOfConsent.True
//             }; 
//             MobileAds.SetRequestConfiguration(requestConfiguration);
//         }
//     }
//}