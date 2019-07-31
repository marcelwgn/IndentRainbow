using System;
using System.Windows.Media;

namespace IndentRainbow.Logic.Colors
{
    public class RainbowBrushGetter : IRainbowBrushGetter
    {

        public Brush[] brushes = new Brush[] {
            new SolidColorBrush(Color.FromArgb(0x40, 255, 255, 0)),
            new SolidColorBrush(Color.FromArgb(0x40, 102, 255, 51)),
            new SolidColorBrush(Color.FromArgb(0x40, 0, 204, 255)),
            new SolidColorBrush(Color.FromArgb(0x40, 153, 51, 255)),
            new SolidColorBrush(Color.FromArgb(0x40, 255, 0, 255)),
            new SolidColorBrush(Color.FromArgb(0x40, 255, 0, 0)),
            new SolidColorBrush(Color.FromArgb(0x40, 255, 170, 0))
        };

        public Brush errorColor = new SolidColorBrush(Color.FromArgb(0x40, 168, 0, 0));

        public Brush GetColorByIndex(int rainbowIndex)
        {
            if (rainbowIndex < 0)
            {
                throw new ArgumentOutOfRangeException("rainbowIndex");
            }
            int index = rainbowIndex % this.brushes.Length;
            return this.brushes[index];
        }

        public Brush GetErrorBrush()
        {
            return errorColor;
        }
    }
}

