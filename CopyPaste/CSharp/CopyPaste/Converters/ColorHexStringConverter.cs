using System.Globalization;
using System.Text.RegularExpressions;
using Sevices.Core.Extensions;

using Color = System.Drawing.Color;

namespace CopyPaste.Converters
{
    internal class ColorHexStringConverter : IValueConverter
    {
        private readonly Regex colorCodeRegex = new Regex("^#[A-Fa-f0-9]{6}$", RegexOptions.Compiled);
        private readonly Regex colorCodeWithAlphaRegex = new Regex("^#[A-Fa-f0-9]{8}$", RegexOptions.Compiled);

        object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return color.ToHex();
            }

            return BindableProperty.UnsetValue;
        }

        object? IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var colorCode = (value as string) ?? string.Empty;
            if (this.colorCodeRegex.IsMatch(colorCode))
            {
                // #RRGGBB
                int r = Convert.ToInt32(colorCode.Substring(1, 2), 16);
                int g = Convert.ToInt32(colorCode.Substring(3, 2), 16);
                int b = Convert.ToInt32(colorCode.Substring(5, 2), 16);
                return Color.FromArgb(r, g, b);
            }

            if (this.colorCodeWithAlphaRegex.IsMatch(colorCode))
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
