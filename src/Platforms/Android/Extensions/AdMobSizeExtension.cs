using Android.Gms.Ads;
using Soenneker.Maui.Admob.Enums;

namespace Soenneker.Maui.Admob.Platforms.Android.Extensions;

public static class AdmobAdSizeExtensions
{
    public static AdSize? ToAdSize(this AdmobAdSize admobAdSize)
    {
        var adSize = admobAdSize.Name switch
        {
            nameof(AdmobAdSize.Banner) => AdSize.Banner,
            nameof(AdmobAdSize.LargeBanner) => AdSize.LargeBanner,
            nameof(AdmobAdSize.MediumRectangle) => AdSize.MediumRectangle,
            nameof(AdmobAdSize.FullBanner) => AdSize.FullBanner,
            nameof(AdmobAdSize.Leaderboard) => AdSize.Leaderboard,
            _ => null
        };

        return adSize;
    }
}