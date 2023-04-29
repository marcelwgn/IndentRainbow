using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace IndentRainbow.Logic.Parser
{
	public static class ColorParser
	{
		public enum ColorMode
		{
			[Description("Solid")]
			Solid = 0,
			[Description("Gradient")]
			Gradient = 1,
		}

		/// <summary>
		/// Converts a given string to a brush array.
		/// Colors must be separated using "," and colors must be in ARGB Hexadecimal format
		/// </summary>
		/// <param name="colors"></param>
		/// <returns></returns>
		public static Color[] ConvertStringToColorArray(string colors, double opacityMultiplier)
		{
			if (string.IsNullOrEmpty(colors))
			{
				return Array.Empty<Color>();
			}
			var splitColors = colors.Split(',');
			var colorCount = splitColors.Length;
			var brushes = new List<Brush>();

			List<Color> colorList = new List<Color>();

			for (var i = 0; i < colorCount; i++)
			{
				try
				{
					var color = (Color)ColorConverter.ConvertFromString(splitColors[i]);
					color.A = (byte)Math.Floor(color.A * opacityMultiplier);
					colorList.Add(color);
				}
				catch (FormatException) { }
			}
			return colorList.ToArray();
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
