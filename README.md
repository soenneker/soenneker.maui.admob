[![](https://img.shields.io/nuget/v/soenneker.maui.admob.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.maui.admob/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.maui.admob/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.maui.admob/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.maui.admob.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.maui.admob/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.maui.admob/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.maui.admob/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Maui.Admob

`Soenneker.Maui.Admob` helps you add Google AdMob support to a .NET MAUI app.

The current implementation is best understood as:

- An Android MAUI `BannerAd` view/handler you can place in XAML or C#
- An iOS `IAdMobService` for loading banner, interstitial, and rewarded ads directly

## Installation

```bash
dotnet add package Soenneker.Maui.Admob
```

## What This Package Provides

### Android

On Android, the package registers a custom MAUI handler for `BannerAd`.

You add the control to your UI, and the handler:

- Initializes `MobileAds`
- Reads configuration from `IConfiguration`
- Chooses either your real banner unit id or the Google test id
- Loads the banner automatically
- Raises load/click/impression/open/close events back to the MAUI control

Supported config keys:

- `AdMob:TestMode`
- `AdMob:BannerUnitId`
- `AdMob:TestDevices`

### iOS

On iOS, the package registers `IAdMobService`.

That service currently supports:

- `Initialize()`
- `LoadBannerAd(...)`
- `LoadInterstitialAd(...)`
- `ShowInterstitialAd()`
- `LoadRewardedAd(...)`
- `ShowRewardedAd(...)`

## Register The Package

In your `MauiProgram.cs`:

```csharp
using Soenneker.Maui.Admob.Registrars;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .AddAdMobService();

        return builder.Build();
    }
}
```

## Android Banner Usage

If your goal is to show a banner ad in a MAUI page, this is the main feature currently wired up.

### 1. Add configuration

Put your AdMob settings somewhere MAUI will load into `IConfiguration`, for example `appsettings.json`:

```json
{
  "AdMob": {
    "TestMode": true,
    "BannerUnitId": "ca-app-pub-xxxxxxxxxxxxxxxx/xxxxxxxxxx",
    "TestDevices": [
      "YOUR_TEST_DEVICE_ID"
    ]
  }
}
```

How those values are used:

- If `TestMode` is `true`, the package uses Google's banner test id
- If `TestMode` is `false`, it uses `BannerUnitId`
- If `TestDevices` is supplied, it passes those ids into the AdMob request configuration

### 2. Add the control to a page

```xml
<ContentPage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:admob="clr-namespace:Soenneker.Maui.Admob;assembly=Soenneker.Maui.Admob"
    x:Class="YourApp.MainPage">

    <VerticalStackLayout Padding="24">
        <Label Text="Content above the ad" />

        <admob:BannerAd Size="Banner" />
    </VerticalStackLayout>
</ContentPage>
```

### 3. Choose a banner size

Available sizes:

- `Banner`
- `LargeBanner`
- `MediumRectangle`
- `FullBanner`
- `Leaderboard`
- `AdaptiveBanner`
- `Custom`

Example:

```xml
<admob:BannerAd Size="LargeBanner" />
```

### 4. Custom size example

If you use `Custom`, set `ContentWidth` and `ContentHeight`:

```xml
<admob:BannerAd
    Size="Custom"
    ContentWidth="320"
    ContentHeight="100" />
```

### 5. Handle banner events

The `BannerAd` control exposes these events:

- `OnLoaded`
- `OnFailedToLoad`
- `OnImpression`
- `OnClicked`
- `OnSwiped`
- `OnOpened`
- `OnClosed`

Example in code-behind:

```csharp
using Soenneker.Maui.Admob;
using Soenneker.Maui.Admob.Enums;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        var banner = new BannerAd
        {
            Size = AdmobAdSize.Banner
        };

        banner.OnLoaded += (_, _) =>
        {
            // Ad loaded successfully
        };

        banner.OnFailedToLoad += (_, problem) =>
        {
            var message = problem.Title;
        };

        Content = banner;
    }
}
```

## iOS Service Usage

If you want to work directly with the iOS AdMob SDK wrapper, inject `IAdMobService`.

```csharp
using Soenneker.Maui.Admob.Abstract;

public class AdPage : ContentPage
{
    private readonly IAdMobService _adMobService;

    public AdPage(IAdMobService adMobService)
    {
        _adMobService = adMobService;

        _adMobService.Initialize();
    }
}
```

### Load a banner on iOS

`LoadBannerAd()` expects:

- An ad unit id
- A MAUI `View` whose platform view will host the native banner

Example:

```csharp
using Soenneker.Maui.Admob.Abstract;

public partial class AdPage : ContentPage
{
    private readonly IAdMobService _adMobService;

    public AdPage(IAdMobService adMobService)
    {
        InitializeComponent();
        _adMobService = adMobService;

        var host = new Grid();
        Content = host;

        _adMobService.Initialize();
        _adMobService.LoadBannerAd("ca-app-pub-xxxxxxxxxxxxxxxx/xxxxxxxxxx", host);
    }
}
```

### Interstitial example

```csharp
_adMobService.Initialize();
_adMobService.LoadInterstitialAd("ca-app-pub-xxxxxxxxxxxxxxxx/xxxxxxxxxx");

_adMobService.OnInterstitialAdLoaded += () =>
{
    _adMobService.ShowInterstitialAd();
};
```

### Rewarded example

```csharp
_adMobService.Initialize();
_adMobService.LoadRewardedAd("ca-app-pub-xxxxxxxxxxxxxxxx/xxxxxxxxxx");

_adMobService.OnRewardedAdLoaded += () =>
{
    _adMobService.ShowRewardedAd(() =>
    {
        // Grant the reward here
    });
};
```

## Blazor Hybrid

If your app is a MAUI Blazor Hybrid app, take a look at [`Soenneker.Maui.Blazor.Bridge`](https://github.com/soenneker/soenneker.maui.blazor.bridge). It is designed to bridge MAUI components into `BlazorWebView`, which may make it easier to use this library from your Blazor UI.

## Summary

Use this package like this today:

- For Android MAUI banners: register `.AddAdMobService()`, configure `AdMob:*`, and place `BannerAd` in your page
- For iOS direct SDK usage: inject `IAdMobService`, call `Initialize()`, then load/show ads manually
