using Soenneker.Maui.Admob.Platforms.Android.Abstract;
using Android.Gms.Ads;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Maui.Admob.Platforms.Android;

public class AdMobServiceUtil : IAdMobServiceUtil
{
    private readonly AsyncSingleton _initializer;

    public AdMobServiceUtil()
    {
        _initializer = new AsyncSingleton((e, _) =>
        {
            MobileAds.Initialize(global::Android.App.Application.Context);

            return new object();
        });
    }

    public void Init()
    {
        _initializer.InitSync();
    }
}