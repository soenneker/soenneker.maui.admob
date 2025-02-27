using System;
using Android.Views;
using Android.Widget;
using Microsoft.Maui.Platform;
using Microsoft.Maui.Controls;
using Android.Gms.Ads;

public class AdViewWrapper : ContentView
{
    private readonly AdView _nativeAdView;

    public AdViewWrapper(AdView adView)
    {
        _nativeAdView = adView;

        // Convert the native Android view to a Maui-compatible View
        AdView nativeView = _nativeAdView;
        HandlerChanged += OnHandlerChanged;
    }

    private void OnHandlerChanged(object sender, EventArgs e)
    {
        if (Handler?.PlatformView is ViewGroup mauiViewGroup)
        {
            mauiViewGroup.RemoveAllViews();
            mauiViewGroup.AddView(_nativeAdView);
        }
    }
}