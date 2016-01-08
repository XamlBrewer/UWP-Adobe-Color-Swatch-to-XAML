using System;
using Windows.UI;

namespace XamlBrewer.Uwp.ColorSwatchReader.Models
{
    public class NamedColor
    {
        public NamedColor()
        {}

        public NamedColor(int red, int green, int blue, string name)
        {
            Name = name;
            Color = Color.FromArgb((byte)255, (byte)red, (byte)green, (byte)blue);
        }

        public string Name { get; set; }

        public Color Color { get; set; }

        public string AsXamlResource() {
            // Using string interpolation.
            return $"<SolidColorBrush x:Key='{Name}' Color='{Color}' /> ";
        }
    }
}
