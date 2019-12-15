using IndentRainbow.Logic.Drawing;
using IndentRainbow.Logic.Colors;
using System;

namespace IndentRainbow.Logic.Classification
{
    public class AlternatingLineDecorator : BaseLineDecorator
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
            if(string.IsNullOrEmpty(text))
            {
                return;
            }
            int tabSize = this.validator.GetIndentBlockLength();
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
                this.drawer.DrawBackground(start, -validTabLength, this.colorGetter.GetErrorBrush());
                return;
            }
            if (!detectErrors && validTabLength < 0)
            {
                validTabLength = -validTabLength;
            }

            for (int charIndex = start; charIndex < start + validTabLength ; )
            {
                if(charIndex + tabSize >= text.Length )
                {
                    if(text[charIndex] != '\t')
                    {
                        return;
                    }
                    this.drawer.DrawBackground(charIndex, 1, this.colorGetter.GetColorByIndex(rainbowIndex));
                    charIndex++;
                    rainbowIndex++;
                    continue;
                }
                var cutout = text.Substring(charIndex, tabSize);
                var tabCutOut = text.Substring(charIndex, 1);
                if (this.validator.IsValidIndent(cutout))
                {
                    this.drawer.DrawBackground(charIndex, tabSize, this.colorGetter.GetColorByIndex(rainbowIndex));
                    charIndex += tabSize;
                    rainbowIndex++;
                } else if (this.validator.IsValidIndent(tabCutOut))
                {
                    this.drawer.DrawBackground(charIndex, 1, this.colorGetter.GetColorByIndex(rainbowIndex));
                    charIndex++;
                    rainbowIndex++;
                } else
                {
                    break;
                }
            }
        }

        
    }
}
