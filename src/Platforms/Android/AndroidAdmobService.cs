using Android.Gms.Ads;
using Android.Gms.Ads.Interstitial;
using Android.Gms.Ads.Rewarded;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using Soenneker.Maui.Admob.Abstract;
using System;
using System.Threading.Tasks;
using Android.App;

namespace Soenneker.Maui.Admob;

public class AndroidAdmobService : IAdMobService
{
    private AdView _bannerView;
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
        MobileAds.Initialize(global::Android.App.Application.Context);
    }

    public void LoadBannerAd(string adUnitId, View adContainer)
    {
        if (adContainer is not Grid gridContainer)
            throw new ArgumentException("Ad container must be a Grid on Android");

        Activity? context = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
        _bannerView = new AdView(context)
        {
            AdSize = AdSize.Banner,
            AdUnitId = adUnitId
        };

        _bannerView.AdListener = new AndroidAdListener(
            () => OnBannerAdLoaded?.Invoke(),
            error => OnBannerAdFailedToLoad?.Invoke(error),
            () => OnBannerAdClicked?.Invoke()
        );

        _bannerView.LoadAd(new AdRequest.Builder().Build());

        var wrappedView = new AdViewWrapper(_bannerView);

        gridContainer.Add(wrappedView);
    }


    public void LoadInterstitialAd(string adUnitId)
    {
        Activity? context = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
        AdRequest adRequest = new AdRequest.Builder().Build();

        InterstitialAd.Load(context, adUnitId, adRequest, new InterstitialAdLoadCallbackImpl(this));
    }

    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Show(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity);
            _interstitialAd = null;
            OnInterstitialAdClosed?.Invoke();
        }
    }

    public void LoadRewardedAd(string adUnitId)
    {
        Activity? context = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
        AdRequest adRequest = new AdRequest.Builder().Build();

        RewardedAd.Load(context, adUnitId, adRequest, new RewardedAdLoadCallbackImpl(this));
    }

    public void ShowRewardedAd(Action rewardCallback)
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Show(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity, new UserEarnedRewardListener(this, rewardCallback));
            _rewardedAd = null;
            OnRewardedAdClosed?.Invoke();
        }
    }

    private class InterstitialAdLoadCallbackImpl : InterstitialAdLoadCallback
    {
        private readonly AndroidAdmobService _service;

        public InterstitialAdLoadCallbackImpl(AndroidAdmobService service)
        {
            _service = service;
        }

        public override void OnAdLoaded(Java.Lang.Object p0)
        {
            if (p0 is InterstitialAd interstitialAd)
            {
                _service._interstitialAd = interstitialAd;
                _service.OnInterstitialAdLoaded?.Invoke();
            }
        }

        public override void OnAdFailedToLoad(LoadAdError error)
        {
            _service.OnInterstitialAdFailedToLoad?.Invoke(error.Message);
        }
    }


    private class RewardedAdLoadCallbackImpl : RewardedAdLoadCallback
    {
        private readonly AndroidAdmobService _service;

        public RewardedAdLoadCallbackImpl(AndroidAdmobService service)
        {
            _service = service;
        }

        public override void OnAdLoaded(Java.Lang.Object p0)
        {
            if (p0 is RewardedAd rewardedAd)
            {
                _service._rewardedAd = rewardedAd;
                _service.OnRewardedAdLoaded?.Invoke();
            }
        }

        public override void OnAdFailedToLoad(LoadAdError error)
        {
            _service.OnRewardedAdFailedToLoad?.Invoke(error.Message);
        }
    }

    private class UserEarnedRewardListener : Java.Lang.Object, IOnUserEarnedRewardListener
    {
        private readonly AndroidAdmobService _service;
        private readonly Action _rewardCallback;

        public UserEarnedRewardListener(AndroidAdmobService service, Action rewardCallback)
        {
            _service = service;
            _rewardCallback = rewardCallback;
        }

        public void OnUserEarnedReward(IRewardItem p0)
        {
            _service.OnRewardEarned?.Invoke();
            _rewardCallback?.Invoke();
        }
    }

    private class AndroidAdListener : AdListener
    {
        private readonly Action _onAdLoaded;
        private readonly Action<string> _onAdFailedToLoad;
        private readonly Action _onAdClicked;

        public AndroidAdListener(Action onAdLoaded, Action<string> onAdFailedToLoad, Action onAdClicked)
        {
            _onAdLoaded = onAdLoaded;
            _onAdFailedToLoad = onAdFailedToLoad;
            _onAdClicked = onAdClicked;
        }

        public override void OnAdLoaded()
        {
            _onAdLoaded?.Invoke();
        }

        public override void OnAdFailedToLoad(LoadAdError error)
        {
            _onAdFailedToLoad?.Invoke(error.Message);
        }

        public override void OnAdClicked()
        {
            _onAdClicked?.Invoke();
        }
    }
}