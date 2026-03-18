using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Soenneker.Maui.Admob.Registrars;
using System.Runtime.Versioning;

namespace Soenneker.Maui.Admob.Demo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();

#if ANDROID
            ConfigureAndroidAdMob(builder);
#endif

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if ANDROID
#endif

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

#if ANDROID
        [SupportedOSPlatform("Android")]
        private static void ConfigureAndroidAdMob(MauiAppBuilder builder)
        {
            builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["AdMob:TestMode"] = "true"
            });

            builder.AddAdMobService();
        }
#endif
    }
}
