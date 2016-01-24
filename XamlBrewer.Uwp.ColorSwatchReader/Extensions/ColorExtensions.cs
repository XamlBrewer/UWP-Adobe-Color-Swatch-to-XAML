using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace XamlBrewer.Uwp
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Returns the brightness of a Color, in Luma.
        /// </summary>
        /// <seealso cref="https://en.wikipedia.org/wiki/Luma_(video)"/>
        public static double Luminance(this Color color)
        {
            return .2126 * color.R / 255 + .7152 * color.G / 255 + .0722 * color.B / 255;
        }

        /// <summary>
        /// Returns the most appropriate color for text against a background color.
        /// </summary>
        /// <returns>White of Black.</returns>
        public static Color ContrastingTextColor(this Color color)
        {
            return color.Luminance() < .5 ? Colors.White : Colors.Black;
        }
    }
}
