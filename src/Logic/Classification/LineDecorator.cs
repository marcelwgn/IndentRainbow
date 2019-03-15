using IndentRainbow.Logic.Drawing;
using IndentRainbow.Logic.Colors;
using System;

namespace IndentRainbow.Logic.Classification
{
    public class LineDecorator : ILineDecorator
    {

        private readonly IBackgroundTextIndexDrawer drawer;
        private readonly IRainbowBrushGetter colorGetter;
        private readonly IIndentValidator validator;

        public LineDecorator(IBackgroundTextIndexDrawer drawer, IRainbowBrushGetter colorGetter, IIndentValidator validator)
        {
            this.drawer = drawer;
            this.colorGetter = colorGetter;
            this.validator = validator;
        }

        /// <summary>
        /// Process the given string and draws the relevant highlighting at the right position, 
        /// if the <see cref="IIndentValidator"/> validates the positions
        /// </summary>
        /// <param name="text">The string of the text</param>
        /// <param name="start">The starting position of the line</param>
        /// <param name="end">The ending position of the line</param>
        public void DecorateLine(string text, int start, int end)
        {
            int tabSize = this.validator.GetIndentBlockLength();

            if (start < 0 || start > text.Length)
            {
                throw new ArgumentOutOfRangeException("start");
            }
            if (end < 0 || end > text.Length)
            {
                throw new ArgumentOutOfRangeException("end");
            }
            if (start > end)
            {
                throw new ArgumentException("Start index must be lower than end index");
            }


            int rainbowIndex = 0;

            for (int charIndex = start; charIndex < end - tabSize + 1; charIndex += tabSize)
            {
                var cutout = text.Substring(charIndex, tabSize);
                if (this.validator.IsValidIndent(cutout))
                {
                    this.drawer.DrawBackground(charIndex, tabSize, this.colorGetter.GetColorByIndex(rainbowIndex));
                    rainbowIndex++;
                } else
                {
                    break;
                }
            }
        }
    }
}
