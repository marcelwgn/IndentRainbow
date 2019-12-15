using IndentRainbow.Logic.Colors;
using IndentRainbow.Logic.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndentRainbow.Logic.Classification
{
    public abstract class LineDecoratorBase : ILineDecorator
    {

        internal readonly IBackgroundTextIndexDrawer drawer;
        internal readonly IRainbowBrushGetter colorGetter;
        internal readonly IIndentValidator validator;
        // To make testing easier, we use a setter for this.
#pragma warning disable CA1051 // Do not declare visible instance fields
        public bool detectErrors = true;
#pragma warning restore CA1051 // Do not declare visible instance fields

        public LineDecoratorBase(IBackgroundTextIndexDrawer drawer, IRainbowBrushGetter colorGetter, IIndentValidator validator)
        {
            this.drawer = drawer;
            this.colorGetter = colorGetter;
            this.validator = validator;
        }

        public abstract void DecorateLine(string text, int startIndex, int endIndex);

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
        protected int GetIndentLengthIfValid(string text, int start, int end)
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
                }
                else if (this.validator.IsValidIndent(tabCutOut))
                {
                    charIndex -= (tabSize - 1);
                    validTabLength += 1;
                }
                else
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
                int index = 0;
                while (index < cutOut.Length && (cutOut[index] == ' ' || cutOut[index] == '\t'))
                {
                    index++;
                    validTabLength++;
                }
                if (this.validator.IsIncompleteIndent(cutOut))
                {
                    validTabLength *= -1;
                }
            }

            return validTabLength;
        }
    }
}
