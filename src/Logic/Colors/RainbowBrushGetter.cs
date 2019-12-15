using System;
using System.Windows.Media;

namespace IndentRainbow.Logic.Colors
{
    public class RainbowBrushGetter : IRainbowBrushGetter
    {

        private readonly Brush[] Brushes = new Brush[] {
            new SolidColorBrush(Color.FromArgb(0x40, 255, 255, 0)),
            new SolidColorBrush(Color.FromArgb(0x40, 102, 255, 51)),
            new SolidColorBrush(Color.FromArgb(0x40, 0, 204, 255)),
            new SolidColorBrush(Color.FromArgb(0x40, 153, 51, 255)),
            new SolidColorBrush(Color.FromArgb(0x40, 255, 0, 255)),
            new SolidColorBrush(Color.FromArgb(0x40, 255, 0, 0)),
            new SolidColorBrush(Color.FromArgb(0x40, 255, 170, 0))
        };

        private readonly Brush ErrorColor = new SolidColorBrush(Color.FromArgb(0x40, 168, 0, 0));

        public RainbowBrushGetter() { }

        public RainbowBrushGetter(Brush[] brushes, Brush errorColor)
        {
            this.Brushes = brushes;
            this.ErrorColor = errorColor;
        }

        public Brush GetColorByIndex(int rainbowIndex)
        {
            if (rainbowIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rainbowIndex));
            }
            if (this.Brushes.Length == 0)
            {
                return null;
            }
            int index = rainbowIndex % this.Brushes.Length;
            return this.Brushes[index];
        }

        public Brush GetErrorBrush()
        {
            return ErrorColor;
        }
    }
}

