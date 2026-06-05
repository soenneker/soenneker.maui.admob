using Android.Gms.Ads;
using Soenneker.Maui.Admob.Enums;

namespace Soenneker.Maui.Admob.Platforms.Android.Extensions;

/// <summary>
/// Represents the admob ad size extensions.
/// </summary>
public static class AdmobAdSizeExtensions
{
    /// <summary>
    /// Executes the to ad size operation.
    /// </summary>
    /// <param name="admobAdSize">The admob ad size.</param>
    /// <returns>The result of the operation.</returns>
    public static AdSize? ToAdSize(this AdmobAdSize admobAdSize)
    {
        AdSize? adSize = admobAdSize.Name switch
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