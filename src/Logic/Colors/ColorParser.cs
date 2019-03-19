using System.Windows.Media;

namespace IndentRainbow.Logic.Colors
{
    public static class ColorParser
    {
        /// <summary>
        /// Converts a given string to a brush array.
        /// Colors must be seperated using "," and colors must be in ARGB Hexadecimal format
        /// </summary>
        /// <param name="colors"></param>
        /// <returns></returns>
        public static Brush[] ConvertStringToBrushArray(string colors)
        {
            string[] splitColors = colors.Split(',');
            int colorCount = splitColors.Length;
            Brush[] brushes = new Brush[colorCount];

            for (int i = 0; i < colorCount; i++)
            {
                brushes[i] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(splitColors[i]));
            }

            return brushes;
        }
    }
}
