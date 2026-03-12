using Google.MobileAds;
using Microsoft.Maui.Controls;
using System;
using Soenneker.Maui.Admob.Abstract;
using UIKit;

namespace Soenneker.Maui.Admob.Platforms.iOS;

// ReSharper disable once InconsistentNaming
public class IOSAdmobService : IAdMobService
{
    private BannerView _bannerView;
    private InterstitialAd _interstitialAd;
    private RewardedAd _rewardedAd;

    public event Action OnBannerAdLoaded;
    public event Action<string> OnBannerAdFailedToLoad;
    public event Action OnBannerAdClicked;

    public event Action OnInterstitialAdLoaded;
    public event Action<string> OnInterstitialAdFailedToLoad;
    public event Action OnInterstitialAdClosed;

    public event Action OnRewardedAdLoaded;
    public event Action<string> OnRewardedAdFailedToLoad;
    public event Action OnRewardedAdClosed;
    public event Action OnRewardEarned;

    public void Initialize()
    {
        MobileAds.SharedInstance.Start(completionHandler: null);
    }

    public void LoadBannerAd(string adUnitId, View adContainer)
    {
        var viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
        _bannerView = new BannerView(AdSizeCons.Banner, new CoreGraphics.CGPoint(0, 0))
        {
            AdUnitId = adUnitId,
            RootViewController = viewController
        };
        _bannerView.LoadRequest(Request.GetDefaultRequest());
        _bannerView.AdReceived += (sender, args) => OnBannerAdLoaded?.Invoke();
        _bannerView.ReceiveAdFailed += (sender, args) => OnBannerAdFailedToLoad?.Invoke(args.Error.LocalizedDescription);
        ((UIView)adContainer.Handler.PlatformView).AddSubview(_bannerView);
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
            _interstitialAd = ad;
            OnInterstitialAdLoaded?.Invoke();
        });
    }

    public void ShowInterstitialAd()
    {
        var viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
        if (_interstitialAd != null && _interstitialAd.CanPresent(viewController, out _))
        {
            _interstitialAd.Present(viewController);
        }
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
            _rewardedAd = ad;
            OnRewardedAdLoaded?.Invoke();
        });
    }

    public void ShowRewardedAd(Action rewardCallback)
    {
        var viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
        if (_rewardedAd != null && _rewardedAd.CanPresent(viewController, out _))
        {
            _rewardedAd.Present(viewController, () =>
            {
                OnRewardEarned?.Invoke();
                rewardCallback?.Invoke();
            });
        }
    }
}