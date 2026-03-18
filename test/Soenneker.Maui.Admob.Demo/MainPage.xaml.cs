using System.Runtime.Versioning;

namespace Soenneker.Maui.Admob.Demo
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            InitializeBannerDemo();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void InitializeBannerDemo()
        {
#if ANDROID
            InitializeAndroidBannerDemo();
#else
            StatusLabel.Text = "Banner demo is only wired up for Android in this sample.";
#endif
        }

#if ANDROID
        [SupportedOSPlatform("Android")]
        private void InitializeAndroidBannerDemo()
        {
            PlatformNoteLabel.Text = "Android is using the Google AdMob test banner.";

            var banner = new BannerAd
            {
                Size = Enums.AdmobAdSize.Banner
            };

            banner.OnLoaded += (_, _) => MainThread.BeginInvokeOnMainThread(() =>
            {
                StatusLabel.Text = "Banner loaded successfully.";
            });

            banner.OnFailedToLoad += (_, problem) => MainThread.BeginInvokeOnMainThread(() =>
            {
                StatusLabel.Text = $"Banner failed to load: {problem.Title}";
            });

            banner.OnClicked += (_, _) => MainThread.BeginInvokeOnMainThread(() =>
            {
                StatusLabel.Text = "Banner clicked.";
            });

            banner.OnImpression += (_, _) => MainThread.BeginInvokeOnMainThread(() =>
            {
                StatusLabel.Text = "Banner impression recorded.";
            });

            BannerHost.Children.Add(banner);
        }
#endif
    }
}
