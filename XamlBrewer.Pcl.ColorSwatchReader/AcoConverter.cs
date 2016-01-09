// Adapted from:
// Reading PhotoShop Color Swatch (aco) files using C#
// http://cyotek.com/blog/reading-photoshop-color-swatch-aco-files-using-csharp

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XamlBrewer.Pcl.ColorSwatchReader
{
    /// <summary>
    /// Parses the content of an Adobe Color Swatch file. 
    /// </summary>
    public class AcoConverter
    {
        /// <summary>
        /// Returns the content of an Adobe Color Swatch file.
        /// </summary>
        public List<SwatchColor> ReadPhotoShopSwatchFile(Stream fileContent)
        {
            List<SwatchColor> colorPalette;

            using (var stream = fileContent)
            {
                FileVersion version;

                // read the version, which occupies two bytes
                version = (FileVersion)this.ReadInt16(stream);

                if (version != FileVersion.Version1 && version != FileVersion.Version2)
                    throw new InvalidDataException("Invalid version information.");

                // the specification states that a version2 palette follows a version1
                // the only difference between version1 and version2 is the inclusion 
                // of a name property. Perhaps there's additional color spaces as well
                // but we can't support them all anyway
                // I noticed some files no longer include a version 1 palette

                colorPalette = this.ReadSwatches(stream, version);
                if (version == FileVersion.Version1)
                {
                    version = (FileVersion)this.ReadInt16(stream);
                    if (version == FileVersion.Version2)
                        colorPalette = this.ReadSwatches(stream, version);
                }
            }

            return colorPalette;
        }

        /// <summary>
        /// Reads a 16bit unsigned integer in big-endian format.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <returns>The unsigned 16bit integer cast to an <c>Int32</c>.</returns>
        private int ReadInt16(Stream stream)
        {
            return (stream.ReadByte() << 8) | (stream.ReadByte() << 0);
        }

        /// <summary>
        /// Reads a 32bit unsigned integer in big-endian format.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <returns>The unsigned 32bit integer cast to an <c>Int32</c>.</returns>
        private int ReadInt32(Stream stream)
        {
            return ((byte)stream.ReadByte() << 24) | ((byte)stream.ReadByte() << 16) | ((byte)stream.ReadByte() << 8) | ((byte)stream.ReadByte() << 0);
        }

        /// <summary>
        /// Reads a unicode string of the specified length.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <param name="length">The number of characters in the string.</param>
        /// <returns>The string read from the stream.</returns>
        private string ReadString(Stream stream, int length)
        {
            byte[] buffer;

            buffer = new byte[length * 2];

            stream.Read(buffer, 0, buffer.Length);

            return Encoding.BigEndianUnicode.GetString(buffer, 0, buffer.Length - 2);
        }

        private List<SwatchColor> ReadSwatches(Stream stream, FileVersion version)
        {
            int colorCount;
            List<SwatchColor> results;

            results = new List<SwatchColor>();

            // read the number of colors, which also occupies two bytes
            colorCount = this.ReadInt16(stream);

            for (int i = 0; i < colorCount; i++)
            {
                ColorSpace colorSpace;
                int value1;
                int value2;
                int value3;
                int value4;
                string name = "color_" + i.ToString();

                // again, two bytes for the color space
                colorSpace = (ColorSpace)(this.ReadInt16(stream));

                value1 = this.ReadInt16(stream);
                value2 = this.ReadInt16(stream);
                value3 = this.ReadInt16(stream);
                value4 = this.ReadInt16(stream);

                if (version == FileVersion.Version2)
                {
                    int length;

                    // need to read the name even though currently our colour collection doesn't support names
                    length = ReadInt32(stream);
                    if (length > 0)
                    {
                        name = this.ReadString(stream, length);
                    }

                }

                switch (colorSpace)
                {
                    case ColorSpace.Rgb:
                        int red;
                        int green;
                        int blue;

                        // RGB.
                        // The first three values in the color data are red , green , and blue . They are full unsigned
                        //  16-bit values as in Apple's RGBColor data structure. Pure red = 65535, 0, 0.

                        red = value1 / 256; // 0-255
                        green = value2 / 256; // 0-255
                        blue = value3 / 256; // 0-255

                        results.Add(new SwatchColor() { Red = red, Blue = blue, Green = green, Name = name });
                        break;

                    case ColorSpace.Hsb:
                        double hue;
                        double saturation;
                        double brightness;

                        // HSB.
                        // The first three values in the color data are hue , saturation , and brightness . They are full 
                        // unsigned 16-bit values as in Apple's HSVColor data structure. Pure red = 0,65535, 65535.

                        hue = value1 / 182.04; // 0-359
                        saturation = value2 / 655.35; // 0-100
                        brightness = value3 / 655.35; // 0-100

                        throw new InvalidDataException(string.Format("Color space '{0}' not supported.", colorSpace));

                    case ColorSpace.Grayscale:

                        int gray;

                        // Grayscale.
                        // The first value in the color data is the gray value, from 0...10000.

                        gray = (int)(value1 / 39.0625); // 0-255

                        results.Add(new SwatchColor() { Red = gray, Blue = gray, Green = gray, Name = name });
                        break;

                    default:
                        throw new InvalidDataException(string.Format("Color space '{0}' not supported.", colorSpace));
                }
            }

            return results;
        }

        #region Enums

        private enum ColorSpace
        {
            Rgb = 0,
            Hsb = 1,
            Cmyk = 2,
            Lab = 7,
            Grayscale = 8
        }

        private enum FileVersion
        {
            Version1 = 1,
            Version2
        }

        #endregion
    }
}
