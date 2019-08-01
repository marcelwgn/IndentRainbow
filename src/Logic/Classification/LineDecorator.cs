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
        public bool detectErrors = true;

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
            int validTabLength = GetIndentLengthIfValid(text, start, end);

                     if (validTabLength < 0 & detectErrors)
            {
                this.drawer.DrawBackground(start, -validTabLength, this.colorGetter.GetErrorBrush());
                return;
            }
            if (!detectErrors && validTabLength < 0)
            {
                validTabLength = -validTabLength;
            }

            for (int charIndex = start; charIndex < start + validTabLength; charIndex += tabSize)
            {
                var cutout = text.Substring(charIndex, tabSize);
                var tabCutOut = text.Substring(charIndex, 1);
                if (this.validator.IsValidIndent(cutout))
                {
                    this.drawer.DrawBackground(charIndex, tabSize, this.colorGetter.GetColorByIndex(rainbowIndex));
                    rainbowIndex++;
                } else if (this.validator.IsValidIndent(tabCutOut))
                {
                    this.drawer.DrawBackground(charIndex, 1, this.colorGetter.GetColorByIndex(rainbowIndex));
                    charIndex -= (tabSize - 1);
                    rainbowIndex++;
                } else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Calculates the lenght of the indentation block.
        /// Returns the value positive if indentation is valid, otherwise returns the value as negative number
        /// For example the text "    text" (4 spaces) would return 4 for indent length 4, 
        /// but the text "     text" (5 spaces) returns -5 for indent length 4.
        /// </summary>
        /// <remarks>
        /// For perfomance reasons, instead of returning a Tuple containing a boolean and an integer, 
        /// the method returns just one integer.
        /// </remarks>
        /// <param name="text">Text containing the line to be analyzed</param>
        /// <param name="start">Start of the line</param>
        /// <param name="end">End of the line</param>
        /// <returns>The length of the valid indent block if the indentation is valid, 
        /// otherwise the valid indent length times -1</returns>
        private int GetIndentLengthIfValid(string text, int start, int end)
        {
            int tabSize = this.validator.GetIndentBlockLength();
            int validTabLength = 0;
            int charIndex = start;
            for (; charIndex < end - tabSize + 1; charIndex += tabSize)
            {
                var cutOut = text.Substring(charIndex, tabSize);
                var tabCutOut = text.Substring(charIndex, 1);
                if (this.validator.IsValidIndent(cutOut))
                {
                    validTabLength += tabSize;
                } else if (this.validator.IsValidIndent(tabCutOut))
                {
                    charIndex -= (tabSize - 1);
                    validTabLength += 1;
                } else
                {
                    if (this.validator.IsIncompleteIndent(cutOut))
                    {
                        int index = 0;
                        while (index < cutOut.Length && (cutOut[index] == ' ' || cutOut[index] == '\t'))
                        {
                            index++;
                            validTabLength++;
                        }
                        validTabLength *= -1;
                    }
                    break;
                }
            }
            if (end - charIndex < tabSize)
            {
                //Checking if the last rest of the text is a valid indent
                var cutOut = text.Substring(charIndex, end - charIndex);
                if (this.validator.IsIncompleteIndent(cutOut))
                {
                    int index = 0;
                    while (index < cutOut.Length && (cutOut[index] == ' ' || cutOut[index] == '\t'))
                    {
                        index++;
                        validTabLength++;
                    }
                    validTabLength *= -1;
                }
            }

            return validTabLength;
        }
    }
}
