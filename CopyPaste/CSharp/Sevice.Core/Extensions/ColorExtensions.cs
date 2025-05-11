using System.Drawing;

namespace Sevices.Core.Extensions
{
    public static class ColorExtensions
    {
        public static string ToHex(this Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public static string ToHexWithAlpha(this Color color)
        {
            return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }
    }
}
