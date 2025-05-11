using System.Globalization;
using Sevices.Core.Extensions;

using Color = System.Drawing.Color;

namespace CopyPaste.Converters
{
    internal class ColorToBrushConverter : IValueConverter
    {
        /// <inheritdoc />
        object IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return Convert(color);
            }

            return BindableProperty.UnsetValue;
        }

        /// <inheritdoc />
        object IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
            {
                return ConvertBack(brush);
            }

            return Binding.DoNothing;
        }
        private static SolidColorBrush Convert(Color color)
        {
            var mauiColor = Microsoft.Maui.Graphics.Color.FromArgb(color.ToHexWithAlpha());
            return new SolidColorBrush(mauiColor);
        }

        private static Color ConvertBack(SolidColorBrush brush)
        {
            var c = brush.Color;
            return Color.FromArgb((int)(c.Alpha * 255), (int)(c.Red * 255), (int)(c.Green * 255), (int)(c.Blue * 255));

        }
    }

}
