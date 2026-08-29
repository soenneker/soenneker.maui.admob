using System;
using Microsoft.Maui.Controls;

namespace Soenneker.Maui.Admob.Abstract;

/// <summary>
/// Defines the ad mob service contract.
/// </summary>
public interface IAdMobService
{
    /// <summary>
    /// Occurs when on banner ad loaded.
    /// </summary>
    event Action OnBannerAdLoaded;
    /// <summary>
    /// Occurs when on banner ad failed to load.
    /// </summary>
    event Action<string> OnBannerAdFailedToLoad;
    /// <summary>
    /// Occurs when on banner ad clicked.
    /// </summary>
    event Action OnBannerAdClicked;

    /// <summary>
    /// Occurs when on interstitial ad loaded.
    /// </summary>
    event Action OnInterstitialAdLoaded;
    /// <summary>
    /// Occurs when on interstitial ad failed to load.
    /// </summary>
    event Action<string> OnInterstitialAdFailedToLoad;
    /// <summary>
    /// Occurs when on interstitial ad closed.
    /// </summary>
    event Action OnInterstitialAdClosed;

    /// <summary>
    /// Occurs when on rewarded ad loaded.
    /// </summary>
    event Action OnRewardedAdLoaded;
    /// <summary>
    /// Occurs when on rewarded ad failed to load.
    /// </summary>
    event Action<string> OnRewardedAdFailedToLoad;
    /// <summary>
    /// Occurs when on rewarded ad closed.
    /// </summary>
    event Action OnRewardedAdClosed;
    /// <summary>
    /// Occurs when on reward earned.
    /// </summary>
    event Action OnRewardEarned;

    /// <summary>
    /// Initializes the ad mob service so it is ready for use.
    /// </summary>
    void Initialize();
    /// <summary>
    /// Loads banner Ad.
    /// </summary>
    /// <param name="adUnitId">Identifier of the ad unit to target.</param>
    /// <param name="adContainer">Ad Container for the load banner ad operation.</param>
    void LoadBannerAd(string adUnitId, View adContainer);
    /// <summary>
    /// Loads interstitial Ad for the ad mob service.
    /// </summary>
    /// <param name="adUnitId">Identifier of the ad unit to target.</param>
    void LoadInterstitialAd(string adUnitId);
    /// <summary>
    /// Shows interstitial Ad for the ad mob service.
    /// </summary>
    void ShowInterstitialAd();
    /// <summary>
    /// Loads rewarded Ad for the ad mob service.
    /// </summary>
    /// <param name="adUnitId">Identifier of the ad unit to target.</param>
    void LoadRewardedAd(string adUnitId);
    /// <summary>
    /// Shows rewarded Ad for the ad mob service.
    /// </summary>
    /// <param name="rewardCallback">reward Callback to invoke when the operation runs.</param>
    void ShowRewardedAd(Action rewardCallback);
}
