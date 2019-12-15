using IndentRainbow.Logic.Colors;
using IndentRainbow.Logic.Drawing;
using System;

namespace IndentRainbow.Logic.Classification
{
    public class AlternatingLineDecorator : LineDecoratorBase
    {
        public AlternatingLineDecorator(IBackgroundTextIndexDrawer drawer, IRainbowBrushGetter colorGetter, IIndentValidator validator) : base(drawer, colorGetter, validator)
        {
        }

        /// <summary>
        /// Process the given string and draws the relevant highlighting at the right position, 
        /// if the <see cref="IIndentValidator"/> validates the positions
        /// </summary>
        /// <param name="text">The string of the text</param>
        /// <param name="start">The starting position of the line</param>
        /// <param name="end">The ending position of the line</param>
        public override void DecorateLine(string text, int start, int end)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            int tabSize = validator.GetIndentBlockLength();
            if (start < 0 || start > text.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }
            if (end < 0 || end > text.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(end));
            }
            if (start > end)
            {
                // English is fine for exceptions
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                throw new ArgumentException("Start index must be lower than end index");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            }


            int rainbowIndex = 0;
            int validTabLength = GetIndentLengthIfValid(text, start, end);

            if (validTabLength < 0 && detectErrors)
            {
                drawer.DrawBackground(start, -validTabLength, colorGetter.GetErrorBrush());
                return;
            }
            if (!detectErrors && validTabLength < 0)
            {
                validTabLength = -validTabLength;
            }

            for (int charIndex = start; charIndex < start + validTabLength;)
            {
                if (charIndex + tabSize >= text.Length)
                {
                    if (text[charIndex] != '\t')
                    {
                        return;
                    }
                    drawer.DrawBackground(charIndex, 1, colorGetter.GetColorByIndex(rainbowIndex));
                    charIndex++;
                    rainbowIndex++;
                    continue;
                }
                string cutout = text.Substring(charIndex, tabSize);
                string tabCutOut = text.Substring(charIndex, 1);
                if (validator.IsValidIndent(cutout))
                {
                    drawer.DrawBackground(charIndex, tabSize, colorGetter.GetColorByIndex(rainbowIndex));
                    charIndex += tabSize;
                    rainbowIndex++;
                }
                else if (validator.IsValidIndent(tabCutOut))
                {
                    drawer.DrawBackground(charIndex, 1, colorGetter.GetColorByIndex(rainbowIndex));
                    charIndex++;
                    rainbowIndex++;
                }
                else
                {
                    break;
                }
            }
        }


    }
}
