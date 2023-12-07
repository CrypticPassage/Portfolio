using System;

namespace Game.Services.Advertisements
{
    public interface IAdsProvider
    {
        void ShowRewardedAd(Action onReward, Action onError);
    }
}