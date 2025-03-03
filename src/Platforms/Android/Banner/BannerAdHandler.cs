using Android.Gms.Ads;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Handlers;
using Soenneker.Dtos.ProblemDetails;
using Soenneker.Extensions.Enumerable;
using Soenneker.Maui.Admob.Constants;
using Soenneker.Maui.Admob.Enums;
using Soenneker.Maui.Admob.Platforms.Android.Abstract;
using Soenneker.Maui.Admob.Platforms.Android.Extensions;
using System;
using System.Collections.Generic;
using Soenneker.Maui.Admob.Platforms.Android.Banner;

namespace Soenneker.Maui.Admob;

public class BannerAdHandler : ViewHandler<BannerAd, AdView>
{
    public static PropertyMapper<BannerAd, BannerAdHandler> PropertyMapper = new(ViewMapper);

    private List<string>? _testDevices;
    private string _unitId = AdmobUnitIdConstants.Banner;
    private EventHandler<AdError>? _onAdFailedToLoad;

    // Requires empty parameters constructor
    public BannerAdHandler() : base(PropertyMapper)
    {
    }

    protected override AdView CreatePlatformView()
    {
        // Retrieve configuration and initialize the AdMob service before building the view
        if (MauiContext?.Services is IServiceProvider services)
        {
            var config = services.GetService<IConfiguration>();
            var adMobServiceUtil = services.GetService<IAdMobServiceUtil>();

            if (config != null)
            {
                _testDevices = config.GetValue<List<string>?>("AdMob:TestDevices");
                var unitId = config.GetValue<string?>("AdMob:BannerUnitId");
                var testMode = config.GetValue<bool>("AdMob:TestMode");

                _unitId = testMode || string.IsNullOrEmpty(unitId)
                    ? AdmobUnitIdConstants.Banner
                    : unitId;
            }

            adMobServiceUtil?.Init();
        }

        // Build the AdView with the correct AdUnitId and AdSize
        AdView adView = BuildView();
        adView.AdListener = BuildListener();

        // Apply test device configuration if available
        var requestConfigBuilder = new RequestConfiguration.Builder();
        if (_testDevices != null && _testDevices.Populated())
            requestConfigBuilder.SetTestDeviceIds(_testDevices);

        MobileAds.RequestConfiguration = requestConfigBuilder.Build();

        // Load the ad
        adView.LoadAd(new AdRequest.Builder().Build());

        return adView;
    }

    protected override void ConnectHandler(AdView platformView)
    {
        base.ConnectHandler(platformView);

        if (VirtualView != null)
        {
            VirtualView.HeightRequest = platformView.AdSize!.Height;
            VirtualView.WidthRequest = platformView.AdSize!.Width;
        }
    }

    private BannerAdListener BuildListener()
    {
        var listener = new BannerAdListener();
        listener.AdLoaded += VirtualView.OnLoadedFired;

        _onAdFailedToLoad = (s, e) =>
        {
            var adError = new AdError(e.Code, e.Message, e.Domain);
            VirtualView?.OnFailedToLoadFired(s, new ProblemDetailsDto { Title = adError.Message });
        };

        listener.AdFailedToLoad += _onAdFailedToLoad;
        listener.AdImpression += VirtualView.OnImpressionFired;
        listener.AdClicked += VirtualView.OnClickedFired;
        listener.AdOpened += VirtualView.OnOpenedFired;
        listener.AdClosed += VirtualView.OnClosedFired;
        listener.AdSwiped += VirtualView.OnSwipedFired;

        return listener;
    }

    protected override void DisconnectHandler(AdView platformView)
    {
        if (platformView.AdListener is BannerAdListener listener)
        {
            listener.AdLoaded -= VirtualView.OnLoadedFired;

            if (_onAdFailedToLoad != null)
            {
                listener.AdFailedToLoad -= _onAdFailedToLoad;
                _onAdFailedToLoad = null;
            }

            listener.AdImpression -= VirtualView.OnImpressionFired;
            listener.AdClicked -= VirtualView.OnClickedFired;
            listener.AdOpened -= VirtualView.OnOpenedFired;
            listener.AdClosed -= VirtualView.OnClosedFired;
            listener.AdSwiped -= VirtualView.OnSwipedFired;
        }

        platformView.Dispose();
        base.DisconnectHandler(platformView);
    }

    private AdView BuildView()
    {
        AdSize adSize = VirtualView?.Size == AdmobAdSize.Custom
            ? new AdSize(VirtualView.ContentWidth, VirtualView.ContentHeight)
            : VirtualView?.Size.ToAdSize() ?? AdSize.Banner;

        return new AdView(Context)
        {
            AdSize = adSize,
            AdUnitId = _unitId
        };
    }
}
