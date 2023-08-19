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

		/// <summary>
		/// Process the given string and draws the relevant highlighting at the right position, 
		/// if the <see cref="IIndentValidator"/> validates the positions
		/// </summary>
		/// <param name="text">The string of the text</param>
		/// <param name="start">The starting position of the line</param>
		/// <param name="end">The ending position of the line</param>
		public override void DecorateLine(string text, int drawingStartIndex)
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
				drawer.DrawBackground(drawingStartIndex, -validTabLength, colorGetter.ErrorBrush);
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
					drawer.DrawBackground(drawingStartIndex + charIndex, 1, colorGetter.GetColorByIndex(rainbowIndex, colorColumns));
					charIndex++;
					rainbowIndex++;
					continue;
				}
				var cutout = text.Substring(charIndex, tabSize);
				var tabCutOut = text.Substring(charIndex, 1);
				if (validator.IsValidIndent(cutout))
				{
					drawer.DrawBackground(drawingStartIndex + charIndex, tabSize, colorGetter.GetColorByIndex(rainbowIndex, colorColumns));
					charIndex += tabSize;
					rainbowIndex++;
				}
				else if (validator.IsValidIndent(tabCutOut))
				{
					drawer.DrawBackground(drawingStartIndex + charIndex, 1, colorGetter.GetColorByIndex(rainbowIndex, colorColumns));
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
