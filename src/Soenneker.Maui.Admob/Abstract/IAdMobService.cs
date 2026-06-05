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
    /// Executes the initialize operation.
    /// </summary>
    void Initialize();
    /// <summary>
    /// Executes the load banner ad operation.
    /// </summary>
    /// <param name="adUnitId">The ad unit id.</param>
    /// <param name="adContainer">The ad container.</param>
    void LoadBannerAd(string adUnitId, View adContainer);
    /// <summary>
    /// Executes the load interstitial ad operation.
    /// </summary>
    /// <param name="adUnitId">The ad unit id.</param>
    void LoadInterstitialAd(string adUnitId);
    /// <summary>
    /// Executes the show interstitial ad operation.
    /// </summary>
    void ShowInterstitialAd();
    /// <summary>
    /// Executes the load rewarded ad operation.
    /// </summary>
    /// <param name="adUnitId">The ad unit id.</param>
    void LoadRewardedAd(string adUnitId);
    /// <summary>
    /// Executes the show rewarded ad operation.
    /// </summary>
    /// <param name="rewardCallback">The reward callback.</param>
    void ShowRewardedAd(Action rewardCallback);
}