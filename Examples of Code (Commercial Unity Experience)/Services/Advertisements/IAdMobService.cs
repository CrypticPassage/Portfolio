using System;
//using GoogleMobileAds.Api;

namespace Game.Services.Advertisements
{
    public interface IAdMobService
    {
        void ShowRewardedAd(Action<int> onReward, Action onError);
    }
}
