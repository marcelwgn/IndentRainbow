using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndentRainbow.Logic.Colors;
using IndentRainbow.Logic.Drawing;

namespace IndentRainbow.Logic.Classification
{
    public class MonocolorLineDecorator : LineDecoratorBase
    {
        public MonocolorLineDecorator(IBackgroundTextIndexDrawer drawer, IRainbowBrushGetter colorGetter, IIndentValidator validator) : base(drawer, colorGetter, validator)
        {
        }

        public override void DecorateLine(string text, int start, int end)
        {
            if (string.IsNullOrEmpty(text))
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

            int rainbowIndex = -1;
            int validTabLength = GetIndentLengthIfValid(text, start, end);

            if (validTabLength == 0)
            {
                return;
            }
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
                        break;
                    }
                    charIndex++;
                    rainbowIndex++;
                    continue;
                }
                var cutout = text.Substring(charIndex, tabSize);
                var tabCutOut = text.Substring(charIndex, 1);
                if (this.validator.IsValidIndent(cutout))
                {
                    charIndex += tabSize;
                    rainbowIndex++;
                } else if (this.validator.IsValidIndent(tabCutOut))
                {
                    charIndex++;
                    rainbowIndex++;
                } else
                {
                    break;
                }
            }

            this.drawer.DrawBackground(start, validTabLength, this.colorGetter.GetColorByIndex(rainbowIndex));
        }
    }
}
