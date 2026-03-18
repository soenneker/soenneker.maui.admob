[![](https://img.shields.io/nuget/v/soenneker.maui.admob.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.maui.admob/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.maui.admob/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.maui.admob/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.maui.admob.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.maui.admob/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.maui.admob/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.maui.admob/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Maui.Admob

`Soenneker.Maui.Admob` is a .NET MAUI AdMob helper.

The clearest supported path in this repo right now is Android banner ads with `BannerAd`.

## Install

```bash
dotnet add package Soenneker.Maui.Admob
```

### 1. Register the package

This is the same pattern used by the demo:

```csharp
using Microsoft.Extensions.Configuration;
using Soenneker.Maui.Admob.Registrars;

var builder = MauiApp.CreateBuilder();

#if ANDROID
builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
{
    ["AdMob:TestMode"] = "true"
});

builder.AddAdMobService();
#endif
```

If you are not using test mode, configure:

- `AdMob:TestMode`
- `AdMob:BannerUnitId`
- `AdMob:TestDevices`

### 2. Add the Android AdMob app id

In `Platforms/Android/AndroidManifest.xml`:

```xml
<meta-data
    android:name="com.google.android.gms.ads.APPLICATION_ID"
    android:value="ca-app-pub-3940256099942544~3347511713" />
```

That is Google's test app id. Replace it with your own app id for production.

### 3. Add a banner host to your page

```xml
<Grid x:Name="BannerHost" MinimumHeightRequest="60" />
```

### 4. Create the banner in C#

This matches the demo approach:

```csharp
private void InitializeBanner()
{
    var banner = new BannerAd
    {
        Size = Enums.AdmobAdSize.Banner
    };

    banner.OnLoaded += (_, _) =>
    {
        StatusLabel.Text = "Banner loaded successfully.";
    };

    banner.OnFailedToLoad += (_, problem) =>
    {
        StatusLabel.Text = $"Banner failed to load: {problem.Title}";
    };

    BannerHost.Children.Add(banner);
}
```

## Banner Sizes

Available sizes:

- `Banner`
- `LargeBanner`
- `MediumRectangle`
- `FullBanner`
- `Leaderboard`
- `AdaptiveBanner`
- `Custom`

If you use `Custom`, also set `ContentWidth` and `ContentHeight`.

## Banner Events

`BannerAd` exposes:

- `OnLoaded`
- `OnFailedToLoad`
- `OnImpression`
- `OnClicked`
- `OnSwiped`
- `OnOpened`
- `OnClosed`

## Blazor Hybrid

If you are using a MAUI Blazor Hybrid app, you may also want [`Soenneker.Maui.Blazor.Bridge`](https://github.com/soenneker/soenneker.maui.blazor.bridge). It may help you use this library from Blazor code.

## Demo

There is a demo app here:

`test/Soenneker.Maui.Admob.Demo`

It shows the entire Android banner flow:

- registers `AddAdMobService()`
- enables AdMob test mode
- adds the Android AdMob app id
- creates and displays a `BannerAd`
- shows banner load/click/impression status

It uses Google's test ids, so it is safe to run as-is.
