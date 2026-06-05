using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Maui.Hosting;
using Soenneker.Maui.Admob.Abstract;

#if ANDROID
using Soenneker.Maui.Admob.Platforms.Android.Abstract;
using Soenneker.Maui.Admob.Platforms.Android;
using Soenneker.Maui.Admob.Platforms.Android.Banner;
#endif

#if IOS
using Soenneker.Maui.Admob.Platforms.iOS;
#endif

namespace Soenneker.Maui.Admob.Registrars;

/// <summary>
/// Represents the ad mob service registrar.
/// </summary>
public static class AdMobServiceRegistrar
{
    /// <summary>
    /// Adds ad mob service.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns>The result of the operation.</returns>
    public static MauiAppBuilder AddAdMobService(this MauiAppBuilder builder)
    {
        builder.Services.TryAddSingleton<IAdMobService>(serviceProvider =>
        {
#if IOS
                return new IOSAdmobService();
#endif
            return null!;
        });

#if ANDROID
        builder.Services.TryAddSingleton<IAdMobServiceUtil, AdMobServiceUtil>();

        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler(typeof(BannerAd), typeof(BannerAdHandler));
        });
#endif
        return builder;
    }
}