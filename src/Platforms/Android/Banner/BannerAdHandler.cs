using System.Collections.Generic;
using Android.Gms.Ads;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui;
using Microsoft.Maui.Handlers;
using Soenneker.Dtos.ProblemDetails;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.Enumerable;
using Soenneker.Maui.Admob.Constants;
using Soenneker.Maui.Admob.Enums;
using Soenneker.Maui.Admob.Platforms.Android.Abstract;
using Soenneker.Maui.Admob.Platforms.Android.Extensions;

namespace Soenneker.Maui.Admob.Platforms.Android.Banner;

public class BannerAdHandler : ViewHandler<BannerAd, AdView>
{
    public static PropertyMapper<BannerAd, BannerAdHandler> PropertyMapper = new(ViewMapper);

    private readonly List<string>? _testDevices;
    private readonly string _unitId;

    public BannerAdHandler(IConfiguration config, IAdMobServiceUtil adMobServiceUtil) : base(PropertyMapper)
    {
        _testDevices = config.GetValue<List<string>?>("AdMob:TestDevices");
        _unitId = config.GetValueStrict<string>("AdMob:BannerUnitId");
        var testMode = config.GetValue<bool>("AdMob:TestMode");

        if (testMode)
        {
            _unitId = AdmobUnitIdConstants.Banner;
        }

        adMobServiceUtil.Init();
    }

    protected override void DisconnectHandler(AdView platformView)
    {
        platformView.Dispose();
        base.DisconnectHandler(platformView);
    }

    protected override AdView CreatePlatformView()
    {
        var adView = BuildView();

        adView.AdListener = BuildListener();
        
        Load(adView);

        return adView;
    }

    private AdView BuildView()
    {
        AdSize adSize;

        if (VirtualView.Size == AdmobAdSize.Custom)
            adSize = new AdSize(VirtualView.ContentWidth, VirtualView.ContentHeight);
        else
            adSize = VirtualView.Size.ToAdSize();

        var adView = new AdView(Context)
        {
            AdSize = adSize,
            AdUnitId = _unitId
        };

        return adView;
    }

    private BannerAdListener BuildListener()
    {
        var listener = new BannerAdListener();
        listener.AdLoaded += VirtualView.OnLoadedFired;
        listener.AdFailedToLoad += (s, e) => VirtualView.OnFailedToLoadFired(s, new ProblemDetailsDto { Title = e.Message });
        listener.AdImpression += VirtualView.OnImpressionFired;
        listener.AdClicked += VirtualView.OnClickedFired;
        listener.AdOpened += VirtualView.OnOpenedFired;
        listener.AdClosed += VirtualView.OnClosedFired;
        listener.AdSwiped += VirtualView.OnSwipedFired;

        return listener;
    }

    private void Load(AdView adView)
    {
        var configBuilder = new RequestConfiguration.Builder();

        if (_testDevices.Populated())
            configBuilder.SetTestDeviceIds(_testDevices);

        MobileAds.RequestConfiguration = configBuilder.Build();

        var requestBuilder = new AdRequest.Builder();
        AdRequest adRequest = requestBuilder.Build();

        adView.LoadAd(adRequest);

        VirtualView.HeightRequest = adView.AdSize!.Height;
        VirtualView.WidthRequest = adView.AdSize!.Width;
    }
}