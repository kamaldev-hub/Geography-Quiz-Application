using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace GeographyQuizAvalonia.Converters
{
    /// <summary>
    /// Converts a string value to a boolean indicating whether it is null or empty.
    /// Can be used for visibility: true if not null/empty, false otherwise.
    /// </summary>
    public class IsNotNullOrEmptyConverter : IValueConverter
    {
        public static readonly IsNotNullOrEmptyConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (parameter is string strParam && strParam.Equals("Inverted", StringComparison.OrdinalIgnoreCase))
            {
                return value is string s && string.IsNullOrEmpty(s);
            }
            return value is string str && !string.IsNullOrEmpty(str);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// Converts a boolean value to its inverse.
    /// </summary>
    public class BooleanInverterConverter : IValueConverter
    {
        public static readonly BooleanInverterConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return !b;
            }
            return false; // Or AvaloniaProperty.UnsetValue
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return !b;
            }
            throw new NotSupportedException();
        }
    }

    // Placeholder for FeedbackStateToClassConverter if needed later via x:Static
    // public class FeedbackStateToClassConverter : IValueConverter { ... }

}
