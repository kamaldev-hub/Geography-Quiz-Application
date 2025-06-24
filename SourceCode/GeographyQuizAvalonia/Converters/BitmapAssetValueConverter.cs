using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace GeographyQuizAvalonia.Converters
{
    /// <summary>
    /// Converts a string asset path (e.g., "avares://AppName/Assets/image.png") to an IBitmap.
    /// </summary>
    public class BitmapAssetValueConverter : IValueConverter
    {
        public static readonly BitmapAssetValueConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string uriString && !string.IsNullOrEmpty(uriString))
            {
                if (Uri.TryCreate(uriString, UriKind.Absolute, out var uri) && uri != null)
                {
                    try
                    {
                        var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                        if (assets != null && assets.Exists(uri))
                        {
                            return new Bitmap(assets.Open(uri));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading image asset '{uriString}': {ex.Message}");
                        // Optionally return a placeholder error image
                        return null;
                    }
                }
            }
            return null; // Or a default/fallback image
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
