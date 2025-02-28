using Intellenum;

namespace Soenneker.Maui.Admob.Enums;

/// <summary>
/// Represents different AdMob ad sizes for banner advertisements.
/// </summary>
/// <remarks>
/// This class provides predefined ad sizes used in Google AdMob for different placements.
/// </remarks>
[Intellenum<string>]
public partial class AdmobAdSize
{
    /// <summary>
    /// Represents a standard banner ad size (320x50 dp).
    /// </summary>
    public static readonly AdmobAdSize Banner = new("Banner");

    /// <summary>
    /// Represents a large banner ad size (320x100 dp).
    /// </summary>
    public static readonly AdmobAdSize LargeBanner = new("LargeBanner");

    /// <summary>
    /// Represents a medium rectangle ad size (300x250 dp), commonly used in content feeds.
    /// </summary>
    public static readonly AdmobAdSize MediumRectangle = new("MediumRectangle");

    /// <summary>
    /// Represents a full banner ad size (468x60 dp), typically used on tablets.
    /// </summary>
    public static readonly AdmobAdSize FullBanner = new("FullBanner");

    /// <summary>
    /// Represents a leaderboard ad size (728x90 dp), commonly used at the top of the screen.
    /// </summary>
    public static readonly AdmobAdSize Leaderboard = new("Leaderboard");

    /// <summary>
    /// Represents an adaptive banner ad size, which adjusts dynamically to the screen width.
    /// </summary>
    public static readonly AdmobAdSize AdaptiveBanner = new("AdaptiveBanner");

    /// <summary>
    /// Represents a custom ad size that can be manually configured.
    /// </summary>
    public static readonly AdmobAdSize Custom = new("Custom");
}