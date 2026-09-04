using Google.MobileAds;
using Microsoft.Maui.Controls;
using System;
using Soenneker.Maui.Admob.Abstract;
using UIKit;

namespace Soenneker.Maui.Admob.Platforms.iOS;

// ReSharper disable once InconsistentNaming
/// <inheritdoc cref="IAdMobService" />
public class IOSAdmobService : IAdMobService
{
    private BannerView? _bannerView;
    private InterstitialAd? _interstitialAd;
    private RewardedAd? _rewardedAd;

    public event Action? OnBannerAdLoaded;
    public event Action<string>? OnBannerAdFailedToLoad;
    public event Action? OnBannerAdClicked;

    public event Action? OnInterstitialAdLoaded;
    public event Action<string>? OnInterstitialAdFailedToLoad;
    public event Action? OnInterstitialAdClosed;

    public event Action? OnRewardedAdLoaded;
    public event Action<string>? OnRewardedAdFailedToLoad;
    public event Action? OnRewardedAdClosed;
    public event Action? OnRewardEarned;

    public void Initialize()
    {
        MobileAds.SharedInstance.Start(completionHandler: null);
    }

    public void LoadBannerAd(string adUnitId, View adContainer)
    {
        UIViewController viewController = GetRootViewController();
        UIView platformView = adContainer.Handler?.PlatformView as UIView
                              ?? throw new InvalidOperationException("The banner container must be attached to an iOS native view before loading an ad.");

        _bannerView?.RemoveFromSuperview();
        _bannerView?.Dispose();
        _bannerView = new BannerView(AdSizeCons.Banner, new CoreGraphics.CGPoint(0, 0))
        {
            AdUnitId = adUnitId,
            RootViewController = viewController
        };
        _bannerView.AdReceived += (sender, args) => OnBannerAdLoaded?.Invoke();
        _bannerView.ReceiveAdFailed += (sender, args) => OnBannerAdFailedToLoad?.Invoke(args.Error.LocalizedDescription);
        _bannerView.LoadRequest(Request.GetDefaultRequest());
        platformView.AddSubview(_bannerView);
    }

    public void LoadInterstitialAd(string adUnitId)
    {
        var request = Request.GetDefaultRequest();
        InterstitialAd.Load(adUnitId, request, (ad, error) =>
        {
            if (error != null)
            {
                OnInterstitialAdFailedToLoad?.Invoke(error.LocalizedDescription);
                return;
            }
            if (ad is null)
            {
                OnInterstitialAdFailedToLoad?.Invoke("The interstitial ad response did not contain an ad.");
                return;
            }

            _interstitialAd?.Dispose();
            _interstitialAd = ad;
            OnInterstitialAdLoaded?.Invoke();
        });
    }

    public void ShowInterstitialAd()
    {
        if (_interstitialAd is null)
            return;

        UIViewController viewController = GetRootViewController();
        if (_interstitialAd.CanPresent(viewController, out _))
            _interstitialAd.Present(viewController);
    }

    public void LoadRewardedAd(string adUnitId)
    {
        var request = Request.GetDefaultRequest();
        RewardedAd.Load(adUnitId, request, (ad, error) =>
        {
            if (error != null)
            {
                OnRewardedAdFailedToLoad?.Invoke(error.LocalizedDescription);
                return;
            }
            if (ad is null)
            {
                OnRewardedAdFailedToLoad?.Invoke("The rewarded ad response did not contain an ad.");
                return;
            }

            _rewardedAd?.Dispose();
            _rewardedAd = ad;
            OnRewardedAdLoaded?.Invoke();
        });
    }

    public void ShowRewardedAd(Action rewardCallback)
    {
        if (_rewardedAd is null)
            return;

        UIViewController viewController = GetRootViewController();
        if (_rewardedAd.CanPresent(viewController, out _))
        {
            _rewardedAd.Present(viewController, () =>
            {
                OnRewardEarned?.Invoke();
                rewardCallback?.Invoke();
            });
        }
    }

    private static UIViewController GetRootViewController()
    {
        foreach (UIScene scene in UIApplication.SharedApplication.ConnectedScenes)
        {
            if (scene is not UIWindowScene windowScene)
                continue;

            foreach (UIWindow window in windowScene.Windows)
            {
                if (window.IsKeyWindow && window.RootViewController is { } rootViewController)
                    return rootViewController;
            }
        }

        throw new InvalidOperationException("No active iOS window is available to present an ad.");
    }
}
