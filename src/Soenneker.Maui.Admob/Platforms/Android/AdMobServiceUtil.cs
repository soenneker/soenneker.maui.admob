using Soenneker.Maui.Admob.Platforms.Android.Abstract;
using Android.Gms.Ads;
using Soenneker.Asyncs.Initializers;

namespace Soenneker.Maui.Admob.Platforms.Android;

/// <inheritdoc cref="IAdMobServiceUtil"/>
public class AdMobServiceUtil : IAdMobServiceUtil
{
    private readonly AsyncInitializer _initializer;

    public AdMobServiceUtil()
    {
        _initializer = new AsyncInitializer((e) =>
        {
            MobileAds.Initialize(global::Android.App.Application.Context);
        });
    }

    public void Init()
    {
        _initializer.InitSync();
    }
}