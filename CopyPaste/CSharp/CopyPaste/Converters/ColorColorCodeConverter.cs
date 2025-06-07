using System.Globalization;
using System.Text.RegularExpressions;
using Services.Core.Extensions;

using Color = System.Drawing.Color;

namespace CopyPaste.Converters
{
    /// <summary>
    /// Conversion <see cref="Color"/> to RGB/ARGB color code and back.
    /// </summary>
    internal class ColorColorCodeConverter : IValueConverter
    {
        /// <inheritdoc />
        object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return color.GetHtmlColorCode();
            }

            return BindableProperty.UnsetValue;
        }

        /// <inheritdoc />
        object? IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var colorCode = (value as string) ?? string.Empty;
            return ConvertColorCodeToColor(colorCode);
        }

        private static Color ConvertColorCodeToColor(string colorCode)
        {
            if (Regex.IsMatch(colorCode, "^#[A-Fa-f0-9]{6}$"))
            {
                // #RRGGBB
                int r = Convert.ToInt32(colorCode.Substring(1, 2), 16);
                int g = Convert.ToInt32(colorCode.Substring(3, 2), 16);
                int b = Convert.ToInt32(colorCode.Substring(5, 2), 16);
                return Color.FromArgb(r, g, b);
            }

            if (Regex.IsMatch(colorCode, "^#[A-Fa-f0-9]{8}$"))
            {
                // #AARRGGBB
                int a = Convert.ToInt32(colorCode.Substring(1, 2), 16);
                int r = Convert.ToInt32(colorCode.Substring(3, 2), 16);
                int g = Convert.ToInt32(colorCode.Substring(5, 2), 16);
                int b = Convert.ToInt32(colorCode.Substring(7, 2), 16);
                return Color.FromArgb(a, r, g, b);
            }

            return Color.Empty;
        }
    }
}
