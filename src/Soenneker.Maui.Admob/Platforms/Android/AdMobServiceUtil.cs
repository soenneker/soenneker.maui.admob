using Soenneker.Maui.Admob.Platforms.Android.Abstract;
using Android.Gms.Ads;

namespace Soenneker.Maui.Admob.Platforms.Android;

/// <inheritdoc cref="IAdMobServiceUtil"/>
public class AdMobServiceUtil : IAdMobServiceUtil
{
    private readonly object _initializationLock = new();
    private bool _initialized;

    public void Init()
    {
        lock (_initializationLock)
        {
            if (_initialized)
                return;

            MobileAds.Initialize(global::Android.App.Application.Context);
            _initialized = true;
        }
    }
}
