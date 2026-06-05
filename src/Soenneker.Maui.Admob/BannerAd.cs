using System;
using Microsoft.Maui.Controls;
using Soenneker.Dtos.ProblemDetails;
using Soenneker.Maui.Admob.Enums;

namespace Soenneker.Maui.Admob;

/// <summary>
/// Represents the banner ad.
/// </summary>
public class BannerAd : ContentView
{
    /// <summary>
    /// Occurs when on loaded.
    /// </summary>
    public event EventHandler? OnLoaded;
    /// <summary>
    /// Occurs when on failed to load.
    /// </summary>
    public event EventHandler<ProblemDetailsDto>? OnFailedToLoad;
    /// <summary>
    /// Occurs when on impression.
    /// </summary>
    public event EventHandler? OnImpression;
    /// <summary>
    /// Occurs when on clicked.
    /// </summary>
    public event EventHandler? OnClicked;
    /// <summary>
    /// Occurs when on swiped.
    /// </summary>
    public event EventHandler? OnSwiped;
    /// <summary>
    /// Occurs when on opened.
    /// </summary>
    public event EventHandler? OnOpened;
    /// <summary>
    /// Occurs when on closed.
    /// </summary>
    public event EventHandler? OnClosed;

    /// <summary>
    /// The admob ad size property.
    /// </summary>
    public static readonly BindableProperty AdmobAdSizeProperty =
        BindableProperty.Create(nameof(Size), typeof(AdmobAdSize), typeof(BannerAd));

    /// <summary>
    /// The content width property.
    /// </summary>
    public static readonly BindableProperty ContentWidthProperty =
        BindableProperty.Create(nameof(ContentWidth), typeof(int), typeof(BannerAd), 0);

    /// <summary>
    /// The content height property.
    /// </summary>
    public static readonly BindableProperty ContentHeightProperty =
        BindableProperty.Create(nameof(ContentHeight), typeof(int), typeof(BannerAd), 0);

    /// <summary>
    /// Gets or sets size.
    /// </summary>
    public AdmobAdSize? Size
    {
        get => (AdmobAdSize?)GetValue(AdmobAdSizeProperty);
        set => SetValue(AdmobAdSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets content width.
    /// </summary>
    public int ContentWidth
    {
        get => (int)GetValue(ContentWidthProperty);
        set => SetValue(ContentWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets content height.
    /// </summary>
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
