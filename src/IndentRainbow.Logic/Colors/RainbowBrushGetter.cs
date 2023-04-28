using System;
using System.Windows.Media;

namespace IndentRainbow.Logic.Colors
{
    public class RainbowBrushGetter : IRainbowBrushGetter
    {

        private readonly Brush[] brushes;

        private Brush errorBrush;
        public Brush ErrorBrush => errorBrush;

        public RainbowBrushGetter(Brush[] brushes, Brush errorColor)
        {
            this.brushes = brushes;
            this.errorBrush = errorColor;
        }

        public Brush GetColorByIndex(int rainbowIndex)
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

        public Brush GetErrorBrush()
        {
            return ErrorBrush;
        }
    }
}

