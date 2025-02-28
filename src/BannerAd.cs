using System;
using Microsoft.Maui.Controls;
using Soenneker.Dtos.ProblemDetails;
using Soenneker.Maui.Admob.Enums;

namespace Soenneker.Maui.Admob;

public class BannerAd : ContentView
{
    public event EventHandler? OnLoaded;
    public event EventHandler<ProblemDetailsDto>? OnFailedToLoad;
    public event EventHandler? OnImpression;
    public event EventHandler? OnClicked;
    public event EventHandler? OnSwiped;
    public event EventHandler? OnOpened;
    public event EventHandler? OnClosed;

    public static readonly BindableProperty AdmobAdSizeProperty =
        BindableProperty.Create(nameof(Size), typeof(Enums.AdmobAdSize), typeof(BannerAd));

    public static readonly BindableProperty ContentWidthProperty =
        BindableProperty.Create(nameof(ContentWidth), typeof(int), typeof(BannerAd), 0);

    public static readonly BindableProperty ContentHeightProperty =
        BindableProperty.Create(nameof(ContentHeight), typeof(int), typeof(BannerAd), 0);

    public AdmobAdSize? Size
    {
        get => (AdmobAdSize?)GetValue(AdmobAdSizeProperty);
        set => SetValue(AdmobAdSizeProperty, value);
    }

    public int ContentWidth
    {
        get => (int)GetValue(ContentWidthProperty);
        set => SetValue(ContentWidthProperty, value);
    }

    public int ContentHeight
    {
        get => (int)GetValue(ContentHeightProperty);
        set => SetValue(ContentHeightProperty, value);
    }

    internal void OnLoadedFired(object? sender, EventArgs e)
    {
        OnLoaded?.Invoke(sender, e);
    }

    internal void OnFailedToLoadFired(object? sender, ProblemDetailsDto e)
    {
        OnFailedToLoad?.Invoke(sender, e);
    }

    internal void OnImpressionFired(object? sender, EventArgs e)
    {
        OnImpression?.Invoke(sender, e);
    }

    internal void OnClickedFired(object? sender, EventArgs e)
    {
        OnClicked?.Invoke(sender, e);
    }

    internal void OnOpenedFired(object? sender, EventArgs e)
    {
        OnOpened?.Invoke(sender, e);
    }

    internal void OnClosedFired(object? sender, EventArgs e)
    {
        OnClosed?.Invoke(sender, e);
    }

    internal void OnSwipedFired(object? sender, EventArgs e)
    {
        OnSwiped?.Invoke(sender, e);
    }
}
