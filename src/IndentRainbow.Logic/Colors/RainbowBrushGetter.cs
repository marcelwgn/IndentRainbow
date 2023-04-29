using System;
using System.Windows.Media;
using static IndentRainbow.Logic.Parser.ColorParser;
using SWM = System.Windows.Media;

namespace IndentRainbow.Logic.Colors
{
	public class RainbowBrushGetter : IRainbowBrushGetter
	{
		public Brush ErrorBrush => errorBrush;
		private readonly Brush errorBrush;

		private Brush[] brushes;
		private LinearGradientBrush[] fadeOutBrushes;
		private LinearGradientBrush fadeInBrush;
		private LinearGradientBrush fadeInAndOutBrush;

		private readonly bool fadeColors;

		public RainbowBrushGetter(Color[] colors, Brush errorColor, ColorMode colorMode, bool fadeColors)
		{
			if (colors == null || colors.Length == 0)
			{
				return;
			}
			this.fadeColors = fadeColors;

			GenerateBaseBrushes(colors, colorMode);

			if (fadeColors)
			{
				GenerateFadeBrushes(colors, colorMode);
			}

			this.errorBrush = errorColor;
		}

		private void GenerateBaseBrushes(Color[] colors, ColorMode colorMode)
		{
			brushes = new Brush[colors.Length];
			for (int i = 0; i < brushes.Length; i++)
			{
				if (colorMode == ColorMode.Gradient)
				{
					if (i + 1 < colors.Length)
					{
						brushes[i] = new LinearGradientBrush(colors[i], colors[i + 1], 0.0);
					}
					else
					{
						brushes[i] = new LinearGradientBrush(colors[i], colors[0], 0.0);
					}
				}
				else
				{
					brushes[i] = new SolidColorBrush(colors[i]);
				}
			}
		}

		private void GenerateFadeBrushes(Color[] colors, ColorMode colorMode)
		{
			fadeOutBrushes = new LinearGradientBrush[colors.Length];

			for (int i = 0; i < colors.Length; i++)
			{
				fadeOutBrushes[i] = CreateThreeStepBrush(colors[i], colors[i], SWM.Colors.Transparent);
			}

			fadeInAndOutBrush = CreateThreeStepBrush(SWM.Colors.Transparent, colors[0], SWM.Colors.Transparent);

			if (colorMode == ColorMode.Gradient)
			{
				fadeInBrush = CreateThreeStepBrush(SWM.Colors.Transparent, colors[0], colors.Length > 1 ? colors[1] : colors[0]);
			}
			else
			{
				fadeInBrush = CreateThreeStepBrush(SWM.Colors.Transparent, colors[0], colors[0]);
			}
		}

		public Brush GetColorByIndex(int rainbowIndex, int numberOfColorsInRainbow)
		{
			if (rainbowIndex < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(rainbowIndex));
			}
			if (brushes == null || brushes.Length == 0)
			{
				return null;
			}
			var index = rainbowIndex % brushes.Length;
			if (fadeColors && numberOfColorsInRainbow > 0)
			{
				if (rainbowIndex == 0 && rainbowIndex == numberOfColorsInRainbow - 1)
				{
					return fadeInAndOutBrush;
				}
				if (rainbowIndex == 0)
				{
					return fadeInBrush;
				}
				if (rainbowIndex == numberOfColorsInRainbow - 1)
				{
					return fadeOutBrushes[index];
				}
			}
			return brushes[index];
		}

		private static LinearGradientBrush CreateThreeStepBrush(Color firstColor, Color secondColor, Color thirdColor)
		{
			var brush = new LinearGradientBrush
			{
				// This is to set the angle correctly
				StartPoint = new System.Windows.Point(0, 0),
				EndPoint = new System.Windows.Point(1, 0)
			};
			brush.GradientStops.Add(new GradientStop(firstColor, 0));
			brush.GradientStops.Add(new GradientStop(secondColor, 0.5));
			brush.GradientStops.Add(new GradientStop(thirdColor, 1));
			return brush;
		}
	}
}

