using System;
using Android.Gms.Ads;

namespace Soenneker.Maui.Admob.Platforms.Android.Banner;

/// <summary>
/// Custom listener for handling AdMob banner ad events.
/// </summary>
internal class BannerAdListener : AdListener
{
    /// <summary> Raised when an ad is successfully loaded. </summary>
    public event EventHandler? AdLoaded;

    /// <summary> Raised when an ad fails to load. </summary>
    public event EventHandler<AdError>? AdFailedToLoad;

    /// <summary> Raised when an ad impression is recorded. </summary>
    public event EventHandler? AdImpression;

    /// <summary> Raised when an ad is clicked. </summary>
    public event EventHandler? AdClicked;

    /// <summary> Raised when an ad is opened. </summary>
    public event EventHandler? AdOpened;

    /// <summary> Raised when an ad is closed. </summary>
    public event EventHandler? AdClosed;

    /// <summary> Raised when an ad swipe gesture is detected. </summary>
    public event EventHandler? AdSwiped;

    public override void OnAdLoaded()
    {
        base.OnAdLoaded();
        AdLoaded?.Invoke(this, EventArgs.Empty);
    }

    public override void OnAdFailedToLoad(LoadAdError error)
    {
        base.OnAdFailedToLoad(error);
        AdFailedToLoad?.Invoke(this, error);
    }

    public override void OnAdImpression()
    {
        base.OnAdImpression();
        AdImpression?.Invoke(this, EventArgs.Empty);
    }

    public override void OnAdClicked()
    {
        base.OnAdClicked();
        AdClicked?.Invoke(this, EventArgs.Empty);
    }

    public override void OnAdOpened()
    {
        base.OnAdOpened();
        AdOpened?.Invoke(this, EventArgs.Empty);
    }

    public override void OnAdClosed()
    {
        base.OnAdClosed();
        AdClosed?.Invoke(this, EventArgs.Empty);
    }

    public override void OnAdSwipeGestureClicked()
    {
        base.OnAdSwipeGestureClicked();
        AdSwiped?.Invoke(this, EventArgs.Empty);
    }
}