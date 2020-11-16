using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace IndentRainbow.Logic.Parser
{
    public static class ColorParser
    {
        /// <summary>
        /// Converts a given string to a brush array.
        /// Colors must be separated using "," and colors must be in ARGB Hexadecimal format
        /// </summary>
        /// <param name="colors"></param>
        /// <returns></returns>
        public static Brush[] ConvertStringToBrushArray(string colors, double opacityMultiplier, int colorMode)
        {
            if (string.IsNullOrEmpty(colors))
            {
                return Array.Empty<Brush>();
            }
            var splitColors = colors.Split(',');
            var colorCount = splitColors.Length;
            var brushes = new List<Brush>();

            List<Color> c = new List<Color>();

            for (var i = 0; i < colorCount; i++)
            {
                try
                {
                    c.Add((Color)ColorConverter.ConvertFromString(splitColors[i]));
                }
                catch (FormatException) { }
            }

            for (var i = 0; i < c.Count; i++)
            {
                Brush brush;
                if (colorMode == 1)
                {
                    if (i + 1 < colorCount)
                    {
                        brush = new LinearGradientBrush(c[i], c[i + 1], 0.0);
                    }
                    else
                    {
                        brush = new LinearGradientBrush(c[i], c[0], 0.0);
                    }
                }
                else
                {
                    brush = new SolidColorBrush(c[i]);
                }

                double alphaOfBrush = c[i].A;
                var color = c[i];
                color.A = (byte)Math.Floor(alphaOfBrush * opacityMultiplier);
                if (colorMode == 1)
                {
                    brush.Opacity = color.A;
                }
                else
                {
                    ((SolidColorBrush)brush).Color = color;
                }

                brushes.Add(brush);
            }
            return brushes.ToArray();
        }

        public static Brush ConvertStringToBrush(string color, double opacityMultiplier)
        {
            if (string.IsNullOrEmpty(color))
            {
                return null;
            }
            try
            {
                var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
                double alphaOfBrush = (brush.Color.A);
                var brushColor = brush.Color;
                brushColor.A = (byte)Math.Floor(alphaOfBrush * opacityMultiplier);
                brush.Color = brushColor;
                return brush;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}
