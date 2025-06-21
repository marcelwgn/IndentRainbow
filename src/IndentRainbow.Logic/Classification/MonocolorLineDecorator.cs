using System;
using IndentRainbow.Logic.Colors;
using IndentRainbow.Logic.Drawing;

namespace IndentRainbow.Logic.Classification
{
    public class MonocolorLineDecorator : LineDecoratorBase
    {
        public MonocolorLineDecorator(IBackgroundTextIndexDrawer drawer, IRainbowBrushGetter colorGetter, IIndentValidator validator) : base(drawer, colorGetter, validator)
        {
        }

        public override void DecorateLine(string text, int drawStartIndex, int startIndexLine)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            var validTabLength = GetIndentLengthIfValid(text);

            if (validTabLength == 0)
            {
                return;
            }
            if (validTabLength < 0 && detectErrors)
            {
                drawer.DrawBackground(drawStartIndex, -validTabLength, colorGetter.ErrorBrush, startIndexLine);
                return;
            }
            if (!detectErrors && validTabLength < 0)
            {
                validTabLength = -validTabLength;
            }

			var indentationCount = validator.GetIndentLevelCount(text, validTabLength);
            drawer.DrawBackground(drawStartIndex, validTabLength, colorGetter.GetColorByIndex(indentationCount - 1, -1), startIndexLine);
        }
    }
}
