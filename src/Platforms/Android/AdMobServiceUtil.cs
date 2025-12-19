using Soenneker.Maui.Admob.Platforms.Android.Abstract;
using Android.Gms.Ads;
using Soenneker.Utils.AsyncInitializers;

namespace Soenneker.Maui.Admob.Platforms.Android;

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