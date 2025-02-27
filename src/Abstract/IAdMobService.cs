using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Soenneker.Maui.Admob.Abstract
{
    public interface IAdMobService
    {
        event Action OnBannerAdLoaded;
        event Action<string> OnBannerAdFailedToLoad;
        event Action OnBannerAdClicked;

        event Action OnInterstitialAdLoaded;
        event Action<string> OnInterstitialAdFailedToLoad;
        event Action OnInterstitialAdClosed;

        event Action OnRewardedAdLoaded;
        event Action<string> OnRewardedAdFailedToLoad;
        event Action OnRewardedAdClosed;
        event Action OnRewardEarned;

        void Initialize();
        void LoadBannerAd(string adUnitId, View adContainer);
        void LoadInterstitialAd(string adUnitId);
        void ShowInterstitialAd();
        void LoadRewardedAd(string adUnitId);
        void ShowRewardedAd(Action rewardCallback);
    }
}