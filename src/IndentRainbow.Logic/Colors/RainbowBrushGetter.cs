using System;
using System.Windows.Media;
using static IndentRainbow.Logic.Parser.ColorParser;

namespace IndentRainbow.Logic.Colors
{
    public class RainbowBrushGetter : IRainbowBrushGetter
    {

        private readonly Brush[] brushes;

        private Brush errorBrush;
        public Brush ErrorBrush => errorBrush;

        public RainbowBrushGetter(Color[] colors, Brush errorColor, ColorMode colorMode)
        {
            if(colors == null)
            {
                return;
            }
            brushes = new Brush[colors.Length];
            for (int i = 0; i < brushes.Length; i++)
            {
                switch (colorMode)
                {
                    case ColorMode.Solid:
                        brushes[i] = new SolidColorBrush(colors[i]);
                        break;
                    case ColorMode.Gradient:
						if (i + 1 < colors.Length)
						{
                            brushes[i] = new LinearGradientBrush(colors[i], colors[i + 1], 0.0);
						}
						else
						{
							brushes[i] = new LinearGradientBrush(colors[i], colors[0], 0.0);
						}
                        break;
				}
            }
            this.errorBrush = errorColor;
        }

        public Brush GetColorByIndex(int rainbowIndex, int numberOfColorsInRainbow = 0)
        {
            if (rainbowIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rainbowIndex));
            }
            if (brushes.Length == 0)
            {
                return null;
            }
            var index = rainbowIndex % brushes.Length;
            return brushes[index];
        }
    }
}

