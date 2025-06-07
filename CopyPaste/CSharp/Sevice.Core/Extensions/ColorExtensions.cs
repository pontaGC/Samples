using System.Diagnostics;
using System.Drawing;

namespace Services.Core.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Gets a HTML color code for the given color.
        /// </summary>
        /// <param name="color">The source color.</param>
        /// <returns>The html color code (e.g. "#1E90FF").</returns>
        [DebuggerStepThrough]
        public static string GetHtmlColorCode(this Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        /// <summary>
        /// Gets a HTML color code with alpha value for the given color.
        /// </summary>
        /// <param name="color">The source color.</param>
        /// <returns>The html color code (e.g. "#FF1E90FF").</returns>
        [DebuggerStepThrough]
        public static string GetFullHtmlColorCode(this Color color)
        {
            return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }
    }
}
