using System;
using IndentRainbow.Logic.Colors;
using IndentRainbow.Logic.Drawing;

namespace IndentRainbow.Logic.Classification
{
	public class AlternatingLineDecorator : LineDecoratorBase
	{
		public AlternatingLineDecorator(IBackgroundTextIndexDrawer drawer, IRainbowBrushGetter colorGetter, IIndentValidator validator) : base(drawer, colorGetter, validator)
		{
		}

		public override void DecorateLine(string text, int drawingStartIndexText, int drawingStartIndexLine)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			var tabSize = validator.GetIndentBlockLength();

			var rainbowIndex = 0;
			var validTabLength = GetIndentLengthIfValid(text);

			if (validTabLength < 0 && detectErrors)
			{
				drawer.DrawBackground(drawingStartIndexText, -validTabLength, colorGetter.ErrorBrush, drawingStartIndexLine);
				return;
			}
			if (!detectErrors && validTabLength < 0)
			{
				validTabLength = -validTabLength;
			}

			var colorColumns = validator.GetIndentLevelCount(text, validTabLength);
			for (var charIndex = 0; charIndex < validTabLength;)
			{
				if (charIndex + tabSize >= text.Length)
				{
					if (text[charIndex] != '\t')
					{
						return;
					}
					drawer.DrawBackground(drawingStartIndexText + charIndex, 1, colorGetter.GetColorByIndex(rainbowIndex, colorColumns), drawingStartIndexLine);
					charIndex++;
					rainbowIndex++;
					continue;
				}
				var cutout = text.Substring(charIndex, tabSize);
				var tabCutOut = text.Substring(charIndex, 1);
				if (validator.IsValidIndent(cutout))
				{
					drawer.DrawBackground(drawingStartIndexText + charIndex, tabSize, colorGetter.GetColorByIndex(rainbowIndex, colorColumns), drawingStartIndexLine);
					charIndex += tabSize;
					rainbowIndex++;
				}
				else if (validator.IsValidIndent(tabCutOut))
				{
					drawer.DrawBackground(drawingStartIndexText + charIndex, 1, colorGetter.GetColorByIndex(rainbowIndex, colorColumns), drawingStartIndexLine);
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
