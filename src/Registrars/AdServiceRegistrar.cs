using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using Soenneker.Maui.Admob.Abstract;
using Soenneker.Maui.Admob;

namespace Soenneker.Maui.Admob.Registrars
{
    public static class AdServiceRegistrar
    {
        public static MauiAppBuilder AddAdMobService(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IAdMobService>(serviceProvider =>
            {
#if ANDROID
                return new AndroidAdmobService();
#endif

#if IOS
                return new IOSAdmobService();
#endif
                return null;
            });

            return builder;
        }
    }
}